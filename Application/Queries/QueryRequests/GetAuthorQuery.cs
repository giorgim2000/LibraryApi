using Domain.DataTransferObjects.AuthorDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.QueryRequests
{
    public class GetAuthorQuery : IRequest<AuthorDetailsDto?>
    {
        [Required]
        public int Id { get; set; }
    }
}
