using Domain.DataTransferObjects.AuthorDtos;
using Application.Queries.QueryRequests;
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
    public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, IEnumerable<AuthorDto>>
    {
        private readonly IAuthorRepository _authorRepository;
        public GetAuthorsQueryHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<AuthorDto>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
        {
            var authorCollection = _authorRepository.GetQuery();
            if(request.NameSearch != null)
            {
                authorCollection = authorCollection.Where(i => i.FirstName.ToLower().Contains(request.NameSearch.ToLower())
                                                            || i.LastName.ToLower().Contains(request.NameSearch.ToLower()));
            }
                
            return await authorCollection.Select(author => new AuthorDto
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName
            }).ToListAsync();
        }
    }
}
