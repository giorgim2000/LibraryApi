using Domain.DataTransferObjects.RentalHistoryDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.QueryRequests
{
    public class GetUserRentalHistoryQuery : IRequest<List<BookRentalHistoryDto>?>
    {
        public int UserId { get; set; }
        public BookRentStatus? Status { get; set; }
    }
}
