using Domain.Entities.UserAggregate;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataTransferObjects.RentalHistoryDtos
{
    public class BookRentalHistoryDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string BookTitle { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
    }
}
