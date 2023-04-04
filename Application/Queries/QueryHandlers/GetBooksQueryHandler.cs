using Domain.DataTransferObjects.BookDtos;
using Application.Queries.QueryRequests;
using Domain.Entities;
using Infrastructure.Repositories.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.QueryHandlers
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, IEnumerable<BookDto>>
    {
        private readonly IBookRepository _bookRepository;
        public GetBooksQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var bookCollection = await _bookRepository.GetQuery().Include(book => book.Authors).ToListAsync();
            return bookCollection.Select(book => new BookDto(book.Id, book.Title)
            {
                Description = book.Description,
                ImageUrl = book.Image,
                Authors = book.Authors?.Select(author => author.FirstName + " " + author.LastName).ToList()
            }).ToList();
        }
    }
}
