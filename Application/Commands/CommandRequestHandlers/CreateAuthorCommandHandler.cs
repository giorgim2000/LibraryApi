using Application.Commands.CommandRequests;
using Domain.Entities;
using Infrastructure.Repositories.Abstraction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.CommandRequestHandlers
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, bool>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;
        public CreateAuthorCommandHandler(IAuthorRepository authorRepository, IBookRepository bookRepository)
        {
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        public async Task<bool> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            if (request.FirstName == null || request.LastName == null)
                return false;

            Author author = new(request.FirstName, request.LastName, request.BirthDate)
            {
                Books = new List<Book>()
            };

            if (request.BookIds != null)
            {
                foreach (var bookId in request.BookIds)
                {
                    var book = await _bookRepository.GetById(bookId);
                    if (book != null)
                        author.Books.Add(book);
                }
            }

            await _authorRepository.Create(author);
            return await _authorRepository.SaveChangesAsync();
        }
    }
}
