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
    public class GetUserRentalHistoryQueryHandler : IRequestHandler<GetUserRentalHistoryQuery, List<BookRentalHistoryDto>?>
    {
        private readonly IBookRentalHistoryRepository _rentalRepository;
        public GetUserRentalHistoryQueryHandler(IBookRentalHistoryRepository rentalRepository)
        {
            _rentalRepository = rentalRepository ?? throw new ArgumentNullException(nameof(rentalRepository));
        }
        public async Task<List<BookRentalHistoryDto>?> Handle(GetUserRentalHistoryQuery request, CancellationToken cancellationToken)
        {
            var userRentalHistory = _rentalRepository.GetQuery(i => i.UserId == request.UserId);
            if (request.Status != null)
                userRentalHistory = userRentalHistory.Where(i => i.Status == request.Status);

            return await userRentalHistory.Include(i => i.Book).Include(i => i.User).Select(i => new BookRentalHistoryDto
            {
                Id = i.Id,
                BookTitle = i.Book != null ? i.Book.Title : string.Empty,
                Username = i.User != null ? i.User.UserName : string.Empty,
                CreationDate = i.CreationDate,
                Status = i.Status.ToString()
            }).ToListAsync();
        }
    }
}
