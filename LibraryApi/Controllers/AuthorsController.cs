using Application.Commands.CommandRequests;
using Application.Queries.QueryRequests;
using Domain.DataTransferObjects.UserDtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthorsList()
        {
            var authorList = await _mediator.Send(new GetAuthorsQuery());

            return Ok(authorList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id) 
        {
            var author = await _mediator.Send(new GetAuthorQuery { Id = id});
            if (author == null)
                return NotFound();

            return Ok(author);
        }

        [HttpPost]
        [Authorize(Roles = UserType.Admin)]
        public async Task<IActionResult> CreateAuthor([FromForm]CreateAuthorCommand input)
        {
            var result = await _mediator.Send(input);
            if (result)
                return Created("GetAuthor", null);
            else
                return BadRequest();
        }

        [HttpPut]
        [Authorize(Roles = UserType.Admin)]
        public async Task<IActionResult> UpdateAuthor([FromForm]UpdateAuthorCommand input)
        {
            var result = await _mediator.Send(input);
            if (result)
                return Accepted();
            else
                return BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = UserType.Admin)]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var result = await _mediator.Send(new DeleteAuthorCommand { Id= id });
            if (result)
                return Ok();

            return NotFound();
        }
    }
}
