using Domain.DataTransferObjects.UserDtos;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.CommandRequests
{
    public class LoginCommand : IRequest<IdentityResult>
    {
        [Required]
        public LoginDto Input { get; set; } = new();
    }
}
