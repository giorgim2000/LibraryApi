using Domain.DataTransferObjects.RentalHistoryDtos;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implementation
{
    public class BookRentalHistoryRepository : Repository<BookRentalHistory>, IBookRentalHistoryRepository
    {
        public BookRentalHistoryRepository(DataContext context) : base(context) { }

    }
}
