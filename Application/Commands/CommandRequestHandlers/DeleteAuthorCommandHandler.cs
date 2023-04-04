using Application.Commands.CommandRequests;
using Infrastructure.Repositories.Abstraction;
using Infrastructure.Repositories.Implementation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.CommandRequestHandlers
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, bool>
    {
        private readonly IAuthorRepository _authorRepository;
        public DeleteAuthorCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
        }

        public async Task<bool> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _authorRepository.GetById(request.Id);
            if (author != null)
            {
                _authorRepository.Delete(author);
                return await _authorRepository.SaveChangesAsync();
            }

            return false;
        }
    }
}
