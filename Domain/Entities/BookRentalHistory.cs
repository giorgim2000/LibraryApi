using Domain.DataTransferObjects.RentalHistoryDtos;
using Domain.Entities.UserAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BookRentalHistory
    {
        public int Id { get; set; }
        public AppUser? User { get; set; }
        public int UserId { get; set; }
        public Book? Book { get; set; }
        public int BookId { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public BookRentStatus Status { get; set; }
    }
}
