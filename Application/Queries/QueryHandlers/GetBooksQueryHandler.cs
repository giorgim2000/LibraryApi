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
            var bookCollection = _bookRepository.GetQuery();
            if (!string.IsNullOrWhiteSpace(request.TitleSearchWord))
            {
                request.TitleSearchWord = request.TitleSearchWord.Trim();
                bookCollection = bookCollection.Where(c => c.Title.ToLower().Contains(request.TitleSearchWord.ToLower()));
            }
            return await bookCollection.Include(book => book.Authors).Select(book => new BookDto(book.Id, book.Title)
            {
                Description = book.Description,
                ImageUrl = book.Image,
                Authors = book.Authors.Select(author => author.FirstName + " " + author.LastName).ToList()
            }).ToListAsync();
        }
    }
}
