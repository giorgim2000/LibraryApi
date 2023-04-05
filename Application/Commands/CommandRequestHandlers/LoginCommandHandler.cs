using Application.Commands.CommandRequests;
using Domain.Entities.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.CommandRequestHandlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, IdentityResult>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public LoginCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        public async Task<IdentityResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Input.Username);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Code = "404", Description = "Username doesn't exist!" });

            var roles = await _userManager.GetRolesAsync(user);
            var signInResult = await _signInManager.PasswordSignInAsync(user, request.Input.Password, false, true);
            if(signInResult.Succeeded)
            {
                return IdentityResult.Success;
            }

            return IdentityResult.Failed(new IdentityError { Code = "400", Description = "Something went wront!" });
        }
    }
}
