using Domain.DataTransferObjects.BookDtos;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.CommandRequests
{
    public class CreateBookCommand : IRequest<bool>
    {
        [Required, MaxLength(100)]
        public string Title { get; set; } = string.Empty;
        [MaxLength(300)]
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
        public string WebRootPath { get; set; } = string.Empty;  // READONLY RO GAVXADO SHEIZLEBA!!!
        public double? Rating { get; set; }
        public DateTime? Year { get; set; }
        [Required]
        public bool Taken { get; set; } = false;
        public List<int>? AuthorIds { get; set; }
    }
}
