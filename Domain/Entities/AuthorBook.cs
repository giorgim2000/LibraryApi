using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AuthorBook
    {
        public Author? Author { get; set; }
        public int AuthorsId { get; set; }
        public Book? Book { get; set; }
        public int BooksId { get; set; }
    }
}
