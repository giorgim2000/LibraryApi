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
    [Authorize]
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
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _mediator.Send(new GetUserRentalHistoryQuery
            {
                UserId = userId,
                Status = status
            });
            if (result == null || !result.Any())
                return NotFound();

            return Ok(result);
        }

        [HttpGet(nameof(GetRentalHistory))]
        [Authorize(Roles = UserType.Admin)]
        public async Task<IActionResult> GetRentalHistory(int? byUserId, int? byBookId,
                                                        BookRentStatus? status, DateTime? creationDateStart,
                                                        DateTime? creationDateEnd)
        {
            var result = await _mediator.Send(new GetRentalHistoryQuery
            {
                ByUserId = byUserId,
                ByBookId = byBookId,
                Status = status,
                CreationDateStart = creationDateStart,
                CreationDateEnd = creationDateEnd
            });
            if (!result.Any())
                return NotFound();

            return Ok(result);
        }
        [HttpPost(nameof(RentBook))]
        public async Task<IActionResult> RentBook(int bookId)
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _mediator.Send(new RentBookCommand
            {
                UserId = userId,
                BookId = bookId
            });

            if (!result)
                return BadRequest();

            return Ok();
        }
        [HttpPost(nameof(ReturnBook))]
        public async Task<IActionResult> ReturnBook(int bookId)
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _mediator.Send(new ReturnBookCommand
            {
                UserId = userId,
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
