using Domain.DataTransferObjects.RentalHistoryDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.CommandRequests
{
    public class DeleteBookRentalHistoryCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
