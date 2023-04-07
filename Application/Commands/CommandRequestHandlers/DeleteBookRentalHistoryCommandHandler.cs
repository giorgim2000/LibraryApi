using Application.Commands.CommandRequests;
using Domain.DataTransferObjects.RentalHistoryDtos;
using Infrastructure.Repositories.Abstraction;
using Infrastructure.Repositories.Implementation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.CommandRequestHandlers
{
    public class DeleteBookRentalHistoryCommandHandler : IRequestHandler<DeleteBookRentalHistoryCommand, bool>
    {
        private readonly IBookRentalHistoryRepository _rentalRepository;
        private readonly IBookRepository _bookRepository;
        public DeleteBookRentalHistoryCommandHandler(IBookRentalHistoryRepository rentalRepository, IBookRepository bookRepository)
        {
            _rentalRepository = rentalRepository ?? throw new ArgumentNullException(nameof(rentalRepository));
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        public async Task<bool> Handle(DeleteBookRentalHistoryCommand request, CancellationToken cancellationToken)
        {
            
            var record = await _rentalRepository.GetById(request.Id);

            if (record == null)
                return false;

            var bookLastHistoryRecord = await _rentalRepository.GetQuery(i => i.BookId == record.BookId).MaxAsync(i => i.CreationDate);
            var isLastRecord = record.CreationDate == bookLastHistoryRecord;

            _rentalRepository.Delete(record);
            var recordResult = await _rentalRepository.SaveChangesAsync();

            if (!isLastRecord)
                return recordResult;

            var book = await _bookRepository.GetById(record.BookId);
            
            if (book != null)
            {
                _bookRepository.Update(book);
                book.Taken = record.Status == BookRentStatus.Return ? true : false;
                return recordResult && await _bookRepository.SaveChangesAsync();
            }

            return true;
        }
    }
}
