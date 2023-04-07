using Application.Commands.CommandRequests;
using Application.Queries.QueryRequests;
using Domain.DataTransferObjects.RentalHistoryDtos;
using Domain.DataTransferObjects.UserDtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserType.AdminUser)]
    public class RentalsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RentalsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet(nameof(GetUserRentalHistory))]
        public async Task<IActionResult> GetUserRentalHistory(BookRentStatus? status)
        {
            var result = await _mediator.Send(new GetUserRentalHistoryQuery
            {
                UserId = Convert.ToInt32(User.Claims.FirstOrDefault(i => i.ValueType == ClaimTypes.NameIdentifier)?.Value),
                Status = status
            });
            if (result == null || !result.Any())
                return NotFound();

            return Ok(result);
        }

        [HttpGet(nameof(GetRentalHistory))]
        [Authorize(Roles = UserType.Admin)]
        public async Task<IActionResult> GetRentalHistory(GetRentalHistoryQuery input)
        {
            var result = await _mediator.Send(input);
            if (!result.Any())
                return NotFound();

            return Ok();
        }
        [HttpPost(nameof(RentBook))]
        public async Task<IActionResult> RentBook(int bookId)
        {
            var result = await _mediator.Send(new RentBookCommand
            {
                UserId = Convert.ToInt32(User.Claims.FirstOrDefault(i => i.ValueType == ClaimTypes.NameIdentifier)?.Value),
                BookId = bookId
            });

            if (!result)
                return BadRequest();

            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> ReturnBook(int bookId)
        {
            var result = await _mediator.Send(new ReturnBookCommand
            {
                UserId = Convert.ToInt32(User.Claims.FirstOrDefault(i => i.ValueType == ClaimTypes.NameIdentifier)?.Value),
                BookId = bookId
            });
            if (!result)
                return BadRequest();

            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = UserType.Admin)]
        public async Task<IActionResult> DeleteBookRentalHistory(int id)
        {
            var result = await _mediator.Send(new DeleteBookRentalHistoryCommand { Id = id });
            if (result)
                return Ok();

            return BadRequest();
        }
    }
}
