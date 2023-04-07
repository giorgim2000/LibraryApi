using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.CommandRequests
{
    public class ReturnBookCommand : IRequest<bool>
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
    }
}
