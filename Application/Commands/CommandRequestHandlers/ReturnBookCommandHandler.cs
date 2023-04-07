using Application.Commands.CommandRequests;
using Domain.DataTransferObjects.RentalHistoryDtos;
using Infrastructure.Repositories.Abstraction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.CommandRequestHandlers
{
    public class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand, bool>
    {
        private readonly IBookRentalHistoryRepository _rentalRepository;
        private readonly IBookRepository _bookRepository;
        public ReturnBookCommandHandler(IBookRentalHistoryRepository rentalRepository, IBookRepository bookRepository)
        {
            _rentalRepository = rentalRepository ?? throw new ArgumentNullException(nameof(rentalRepository));
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        public async Task<bool> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetById(request.BookId);
            if (book == null || !book.Taken)
                return false;

            await _rentalRepository.Create(new Domain.Entities.BookRentalHistory
            {
                BookId = request.BookId,
                UserId = request.UserId,
                CreationDate = DateTime.Now,
                Status = BookRentStatus.Return
            });
            var rentalResult = await _rentalRepository.SaveChangesAsync();

            _bookRepository.Update(book);
            book.Taken = true;
            var bookResult = await _bookRepository.SaveChangesAsync();

            if (rentalResult && bookResult)
                return true;

            return false;
        }
    }
}
