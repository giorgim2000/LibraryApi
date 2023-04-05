using Application.Commands.CommandRequests;
using Domain.DataTransferObjects.UserDtos;
using Domain.Entities.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.CommandRequestHandlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, IdentityResult>
    {
        private readonly UserManager<AppUser> _userManager;
        public RegisterUserCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<IdentityResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var userEmail = await _userManager.FindByEmailAsync(request.User.Email);
            var userName = await _userManager.FindByNameAsync(request.User.Username);

            if (userEmail != null)
                return IdentityResult.Failed(new IdentityError { Description="Email is already in Use!"});

            if (userName != null)
                return IdentityResult.Failed(new IdentityError { Description = "Username is already in Use!" });

            AppUser user = new() { UserName = request.User.Username, Email = request.User.Email };

            var registrationResult = await _userManager.CreateAsync(user, request.User.Password);

            if(registrationResult.Succeeded)
            {
                var userClaims = new List<Claim>();
                userClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                userClaims.Add(new Claim(ClaimTypes.Email, user.Email));

                await _userManager.AddClaimsAsync(user, userClaims);

                await _userManager.AddToRoleAsync(user, RoleType.User.ToString());
                
                return IdentityResult.Success;
            }

            return IdentityResult.Failed(new IdentityError { Description = "Something Went Wrong!" });
        }
    }
}
