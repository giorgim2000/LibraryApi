using Application.Commands.CommandRequests;
using Application.Queries.QueryRequests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetBookList()
        {
            var bookCollection = await _mediator.Send(new GetBooksQuery());
            if(bookCollection.Count() == 0)
                return NotFound();

            return Ok(bookCollection);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var result = await _mediator.Send(new GetBookQuery { Id = id });
            if(result != null)
                return Ok(result);

            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> CreateBook([FromForm]CreateBookCommand input)
        {
            var result = await _mediator.Send(input);
            if (result)
                return Created("GetBook", null);
            else
                return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBook([FromForm]UpdateBookCommand input)
        {
            var result = await _mediator.Send(input);
            if (result)
                return Accepted();
            else
                return BadRequest();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _mediator.Send(new DeleteBookCommand { Id = id });
            if (result)
                return Ok();

            return NotFound();
        }
    }
}
