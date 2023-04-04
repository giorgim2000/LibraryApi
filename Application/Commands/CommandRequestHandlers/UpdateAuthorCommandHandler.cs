using Application.Commands.CommandRequests;
using Domain.Entities;
using Infrastructure.Repositories.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.CommandRequestHandlers
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, bool>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;
        public UpdateAuthorCommandHandler(IAuthorRepository authorRepository, IBookRepository bookRepository)
        {
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        public async Task<bool> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _authorRepository.GetQuery(author => author.Id == request.Id).Include(a => a.Books).FirstOrDefaultAsync();
            if (author == null)
                return false;

            _authorRepository.Update(author);
            author.FirstName= request.FirstName;
            author.LastName= request.LastName;
            author.BirthDate= request.BirthDate;

            if(!(author.Books?.Select(b => b.Id).ToList() ?? new List<int>()).OrderBy(id => id).SequenceEqual(request.BookIds.OrderBy(id => id)))
            {
                if (request.BookIds == null || request.BookIds.Count == 0)
                    author.Books = null;
                else
                {
                    foreach (var bookId in request.BookIds)
                    {
                        var book = await _bookRepository.GetById(bookId);
                        if (book != null)
                            author.Books?.Add(book);
                    }
                }
            }

            return await _authorRepository.SaveChangesAsync();
        }
    }
}
