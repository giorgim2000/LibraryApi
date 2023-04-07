using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;   
        public string? Description { get; set; }
        public string? Image { get; set; }
        public double? Rating { get; set; }
        public DateTime? Year { get; set; }
        public bool Taken { get; set; }
        public virtual ICollection<Author>? Authors { get; set; }
        public ICollection<BookRentalHistory>? Rentals { get; set; }
        public Book()
        {

        }
        public Book(string title, string? description, double? rating, DateTime? year, bool taken = false)
        {
            Title = title;
            Description = description;
            Rating = rating;
            Year = year;
            Taken = taken;
        }
    }
}
