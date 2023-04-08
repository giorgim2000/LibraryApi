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
        public CreateBookDto Input { get; set; } = new();
        public string WebRootPath { get; set; } = string.Empty;
    }
}
