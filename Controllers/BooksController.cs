using DbOperations.Data;
using Microsoft.AspNetCore.Mvc;

namespace DbOperations.Controllers
{
    [Route("/api/books")]
    [ApiController]
    public class BooksController(AppDbContext appDbContext) : ControllerBase
    {
        [HttpPost("")]
        public async Task<IActionResult> AddNewBook([FromBody] Book model){
            await appDbContext.Books.AddAsync(model);
            await appDbContext.SaveChangesAsync();
            return Ok(model);
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> AddNewBooks([FromBody] List<Book> model)
        {
            await appDbContext.Books.AddRangeAsync(model);
            await appDbContext.SaveChangesAsync();
            return Ok(model);
        }
    }
}
