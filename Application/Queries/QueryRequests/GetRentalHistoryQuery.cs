using Domain.DataTransferObjects.RentalHistoryDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.QueryRequests
{
    public class GetRentalHistoryQuery : IRequest<List<BookRentalHistoryDto>>
    {
        public int? ByUserId { get; set; }
        public int? ByBookId { get; set; }
        public BookRentStatus? Status { get; set; }
        public DateTime? CreationDateStart { get; set; }
        public DateTime? CreationDateEnd { get; set; }
    }
}
