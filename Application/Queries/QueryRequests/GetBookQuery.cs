using Domain.DataTransferObjects.BookDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.QueryRequests
{
    public class GetBookQuery : IRequest<BookDetailsDto?>
    {
        [Required]
        public int Id { get; set; }
    }
}
