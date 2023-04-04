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
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(DataContext context) : base(context) { }


        
    }
}
