using Application.Commands.CommandRequests;
using Domain.DataTransferObjects.UserDtos;
using Domain.Entities.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AuthController(IMediator mediator, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _mediator = mediator;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost(nameof(RegisterUser))]
        public async Task<IActionResult> RegisterUser(RegisterUserDto input)
        {
            var result = await _mediator.Send(new RegisterUserCommand { User = input });
            if (result.Succeeded)
                return Accepted();

            return BadRequest(result.Errors.FirstOrDefault());
        }

        [AllowAnonymous]
        [HttpPost(nameof(LogIn))]
        public async Task<IActionResult> LogIn([FromForm]LoginDto input)
        {
            var result = await _mediator.Send(new LoginCommand { Input = input });
            if (result.Succeeded)
                return Ok();

            if (result.Errors.FirstOrDefault()?.Code == "404")
                return NotFound(result.Errors.FirstOrDefault()?.Description);

            return BadRequest(result.Errors.FirstOrDefault()?.Description);

            //var user = await _userManager.FindByNameAsync(input.Username);
            //if (user == null)
            //    return NotFound();

            //var roles = await _userManager.GetRolesAsync(user);
            //var signInResult = await _signInManager.PasswordSignInAsync(user, input.Password, false, true);
            //if (signInResult.Succeeded)
            //{
            //    return NoContent();
            //}

            //return BadRequest("Something went wront!" );
        }
    }
}
