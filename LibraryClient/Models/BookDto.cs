using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.BookDtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public List<string>? Authors { get; set; }
        public BookDto(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
