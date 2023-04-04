using Application.Models.BookDtos;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text.Json;

namespace LibraryClient.Controllers
{
    public class BooksController : Controller
    {

        public async Task<IActionResult> Index()
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7095");
            var response = await client.GetAsync("/api/Books");
            var data = await response.Content.ReadAsStringAsync();
            var books = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<BookDto>>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if(books != null)
            {
                foreach (var book in books)
                {
                    book.ImageUrl = "https://localhost:7095" + book.ImageUrl;
                }
            }
            
            return View(books);
        }
    }
}
