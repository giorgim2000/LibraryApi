using Application.Queries.QueryRequests;
using Domain.DataTransferObjects.RentalHistoryDtos;
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
    public class GetRentalHistoryQueryHandler : IRequestHandler<GetRentalHistoryQuery, List<BookRentalHistoryDto>>
    {
        private readonly IBookRentalHistoryRepository _rentalRepository;
        public GetRentalHistoryQueryHandler(IBookRentalHistoryRepository rentalRepository)
        {
            _rentalRepository = rentalRepository ?? throw new ArgumentNullException(nameof(rentalRepository));
        }
        public async Task<List<BookRentalHistoryDto>> Handle(GetRentalHistoryQuery request, CancellationToken cancellationToken)
        {
            var rentalHistory = _rentalRepository.GetQuery();
            if (request.ByUserId != null)
                rentalHistory = rentalHistory.Where(i => i.UserId == request.ByUserId);

            if (request.ByBookId != null)
                rentalHistory = rentalHistory.Where(i => i.BookId == request.ByBookId);

            if(request.CreationDateStart != null) 
                rentalHistory = rentalHistory.Where(i => i.CreationDate >=  request.CreationDateStart);

            if (request.CreationDateEnd != null)
                rentalHistory = rentalHistory.Where(i => i.CreationDate <= request.CreationDateEnd);

            if (request.Status != null)
                rentalHistory = rentalHistory.Where(i => i.Status == request.Status);

            return await rentalHistory.Include(i => i.User).Include(i => i.Book).Select(i => new BookRentalHistoryDto
            {
                Id = i.Id,
                Username = i.User.UserName,
                BookTitle = i.Book.Title,
                CreationDate = i.CreationDate
            }).ToListAsync();
        }
    }
}
