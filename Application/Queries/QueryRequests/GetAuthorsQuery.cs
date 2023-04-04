using Domain.DataTransferObjects.AuthorDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.QueryRequests
{
    public class GetAuthorsQuery : IRequest<IEnumerable<AuthorDto>>
    {
    }
}
