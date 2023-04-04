using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataTransferObjects.BookDtos
{
    public class BookDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public double? Rating { get; set; }
        public DateTime? Year { get; set; }
        public List<string>? Authors { get; set; }
        public bool Taken { get; set; }
        public BookDetailsDto(int id, string title, bool taken)
        {
            Id = id;
            Title = title;
            Taken = taken;
        }
    }
}
