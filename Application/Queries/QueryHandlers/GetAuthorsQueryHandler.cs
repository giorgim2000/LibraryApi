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
            var authorCollection = await _authorRepository.GetQuery().Include(a => a.Books).ToListAsync();
            return authorCollection.Select(author => new AuthorDto(author.Id, author.FirstName, author.LastName)).ToList();
        }
    }
}
