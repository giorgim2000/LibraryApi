using Application.Commands.CommandRequests;
using Application.Services;
using Domain.Entities;
using Infrastructure.Repositories.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.CommandRequestHandlers
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, bool>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IImageService _imageService;
        public CreateBookCommandHandler(IBookRepository bookRepository, IAuthorRepository authorRepository,IImageService imageService)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
            _imageService = imageService ?? throw new ArgumentNullException(nameof(imageService));
        }

        public async Task<bool> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            if(request.Input.Title == null)
                return false;

            Book book = new(request.Input.Title, request.Input.Description, request.Input.Rating, request.Input.Year, request.Input.Taken)
            {
                Authors = new List<Author>()
            };

            if(request.Input.Image != null && request.Input.Image.Length > 0)
            {
                book.Image = await _imageService.SavePictureAsync(request.Input.Image, request.WebRootPath);
            }

            if(request.Input.AuthorIds != null)
            {
                foreach (var authorId in request.Input.AuthorIds)
                {
                    var author = await _authorRepository.GetById(authorId);
                    if (author != null)
                        book.Authors.Add(author);
                }
            }

            await _bookRepository.Create(book);
            return await _bookRepository.SaveChangesAsync();

        }
    }
}
