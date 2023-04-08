using Application.Queries.QueryRequests;
using Domain.Entities;
using Domain.DataTransferObjects.AuthorDtos;
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
    public class GetAuthorQueryHandler : IRequestHandler<GetAuthorQuery, AuthorDetailsDto?>
    {
        private readonly IRepository<Author> _authorRepository;
        public GetAuthorQueryHandler(IRepository<Author> authorRepository)
        {
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
        }

        public async Task<AuthorDetailsDto?> Handle(GetAuthorQuery request, CancellationToken cancellationToken)
        {
            var author = await _authorRepository.GetQuery(author => author.Id == request.Id)
                                                .Include(a => a.Books)
                                                .FirstOrDefaultAsync();
            if (author == null)
                return null;

            return new AuthorDetailsDto
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                BirthDate = author.BirthDate,
                Books = author.Books != null ? author.Books.Select(i => i.Title).ToList() : new List<string>()
            };
        }
    }
}
