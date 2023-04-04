using Domain.DataTransferObjects.BookDtos;
using Application.Queries.QueryRequests;
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
    public class GetBookQueryHandler : IRequestHandler<GetBookQuery, BookDetailsDto?>
    {
        private readonly IBookRepository _bookRepository;
        public GetBookQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        public async Task<BookDetailsDto?> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetQuery(book => book.Id == request.Id).Include(b => b.Authors).FirstOrDefaultAsync();
            if (book == null)
                return null;

            return new BookDetailsDto(book.Id, book.Title, book.Taken)
            {
                ImageUrl = book.Image,
                Description = book.Description,
                Rating= book.Rating,
                Year= book.Year,
                Authors = book.Authors?.Select(author => author.FirstName + " " + author.LastName).ToList()
            };
        }
    }
}
