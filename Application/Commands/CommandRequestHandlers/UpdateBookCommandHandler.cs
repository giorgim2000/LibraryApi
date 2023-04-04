using Application.Commands.CommandRequests;
using Application.Services;
using Domain.Entities;
using Infrastructure.Repositories.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.CommandRequestHandlers
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, bool>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IImageService _imageService;
        private readonly IAuthorRepository _authorRepository;
        public UpdateBookCommandHandler(IBookRepository bookRepository, IImageService imageService, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _imageService = imageService ?? throw new ArgumentNullException(nameof(imageService));
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
        }

        public async Task<bool> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetQuery(book => book.Id == request.Id).Include(b => b.Authors).FirstOrDefaultAsync();
            if (book == null)
                return false;

            _bookRepository.Update(book);
            book.Title = request.Title;
            book.Description = request.Description;
            book.Taken = request.Taken;
            book.Year = request.Year;
            book.Rating = request.Rating;
            
            if(!(book.Authors?.Select(a => a.Id).ToList() ?? new List<int>()).OrderBy(id => id).SequenceEqual(request.AuthorIds.OrderBy(id => id)))
            {
                if(request.AuthorIds?.Count == 0 || request.AuthorIds == null)
                {
                    book.Authors = null;
                }
                else
                {
                    book.Authors?.Clear();
                    foreach (var authorId in request.AuthorIds)
                    {
                        var author = await _authorRepository.GetById(authorId);
                        if(author != null)
                            book.Authors?.Add(author);
                    }
                }
                
            }

            if(request.Image != null && request.Image.Length > 0)
            {
                if(book.Image != null)
                    _imageService.DeletePicture(book.Image);

                book.Image = await _imageService.SavePictureAsync(request.Image);
            }

            return await _bookRepository.SaveChangesAsync();
        }
    }
}
