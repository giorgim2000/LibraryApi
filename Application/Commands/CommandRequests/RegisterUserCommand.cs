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
    public class RegisterUserCommand : IRequest<IdentityResult>
    {
        [Required]
        public RegisterUserDto User { get; set; } = new();
    }
}
