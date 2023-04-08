using Application.Commands.CommandRequests;
using Application.Queries.QueryRequests;
using Domain.DataTransferObjects.BookDtos;
using Domain.DataTransferObjects.UserDtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public BooksController(IMediator mediator, IWebHostEnvironment hostingEnvironment)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _hostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));
        }

        [HttpGet]
        public async Task<IActionResult> GetBookList(string? titleSearch)
        {
            var bookCollection = await _mediator.Send(new GetBooksQuery() { TitleSearchWord = titleSearch });
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
        [Authorize(Roles = UserType.Admin)]
        public async Task<IActionResult> CreateBook([FromForm]CreateBookDto input)
        {
            var rootPath = _hostingEnvironment.ContentRootPath;
            var result = await _mediator.Send(new CreateBookCommand { Input = input, WebRootPath = rootPath });
            if (result)
                return Created("GetBook", null);
            else
                return BadRequest();
        }

        [HttpPut]
        [Authorize(Roles = UserType.Admin)]
        public async Task<IActionResult> UpdateBook([FromForm]UpdateBookDto input)
        {
            var rootPath = _hostingEnvironment.ContentRootPath;
            var result = await _mediator.Send(new UpdateBookCommand { Input = input, WebRootPath = rootPath });
            if (result)
                return Accepted();
            else
                return BadRequest();
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = UserType.Admin)]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _mediator.Send(new DeleteBookCommand { Id = id });
            if (result)
                return Ok();

            return NotFound();
        }
    }
}
