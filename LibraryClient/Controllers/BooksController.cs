using Application.Models.BookDtos;
using Domain.DataTransferObjects.BookDtos;
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
            client.BaseAddress = new Uri("https://localhost:44331");
            var response = await client.GetAsync("/api/Books");
            var data = await response.Content.ReadAsStringAsync();
            var books = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Domain.DataTransferObjects.BookDtos.BookDto>>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if(books != null)
            {
                foreach (var book in books)
                {
                    book.ImageUrl = "https://localhost:7095" + book.ImageUrl;
                }
            }
            
            return View(books);
        }

        public async Task<IActionResult> Details([FromRoute]int id)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44331");
            var response = await client.GetAsync($"/api/Books/{id}");
            var data = await response.Content.ReadAsStringAsync();
            var book = System.Text.Json.JsonSerializer.Deserialize<BookDetailsDto>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            

            return View(book);
        }
        //[HttpGet]
        //public async Task<IActionResult> Edit([FromRoute] int id)
        //{
        //    using var client = new HttpClient();
        //    client.BaseAddress = new Uri("https://localhost:44331");
        //    var response = await client.GetAsync($"/api/Books/{id}");
        //    var data = await response.Content.ReadAsStringAsync();
        //    var book = System.Text.Json.JsonSerializer.Deserialize<BookDetailsDto>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        //    return View(book);
        //}
        //[HttpPut]
        //public async Task<IActionResult> Edit([FromForm]BookDetailsDto input)
        //{
        //    using var client = new HttpClient();
        //    client.BaseAddress = new Uri("https://localhost:44331");

        //    var response = await client.PutAsync($"/api/UpdateBook", new StringContent(JsonConvert.SerializeObject(input)));
        //    var data = await response.Content.ReadAsStringAsync();
        //    var book = System.Text.Json.JsonSerializer.Deserialize<BookDetailsDto>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        //    return View(book);
        //}
    }
}
