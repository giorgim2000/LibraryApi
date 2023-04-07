using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataTransferObjects.BookDtos
{
    public class UpdateBookDto
    {
        [Required]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Title { get; set; } = string.Empty;
        [MaxLength(300)]
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
        public double? Rating { get; set; }
        public DateTime? Year { get; set; }
        [Required]
        public bool Taken { get; set; } = false;
        public List<int> AuthorIds { get; set; } = new List<int>();
    }
}
