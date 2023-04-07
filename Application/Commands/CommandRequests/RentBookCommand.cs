﻿using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.CommandRequests
{
    public class RentBookCommand : IRequest<bool>
    {
        [Required]
        public int BookId { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
