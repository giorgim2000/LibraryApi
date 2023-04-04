using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.CommandRequests
{
    public class DeleteBookCommand : IRequest<bool>
    {
        [Required]
        public int Id { get; set; }
    }
}
