using DbOperations.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> AddNewBooks([FromBody] List<Book> model){
            await appDbContext.Books.AddRangeAsync(model);
            await appDbContext.SaveChangesAsync();
            return Ok(model);
        }

        [HttpPut("{bookId}")]
        public async Task<IActionResult> UpdateBook([FromRoute]int bookId, [FromBody] Book model) {
            var book = await appDbContext.Books.FirstOrDefaultAsync(x => x.Id == bookId);
            if (book==null) {
                return NotFound();
            }
            book.Title = model.Title;
            book.Description = model.Description;
            book.NoOfPages = model.NoOfPages;
            book.Author = model.Author;
            await appDbContext.SaveChangesAsync();
            return Ok(model);
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateBookWithSingleQuery([FromBody] Book model) {
            appDbContext.Books.Update(model);
            await appDbContext.SaveChangesAsync();
            return Ok(model);
        }

        [HttpPut("bulk")]
        public async Task<IActionResult> UpdateBooksInBulk(){
            await appDbContext.Books
            .Where(x=>x.NoOfPages>=200)
            .ExecuteUpdateAsync(x => x
            .SetProperty(p=>p.Title,p=>p.Title+"BulkUpdated")
            .SetProperty(p=>p.Description,p=>"Description of " + p.Title)
            );
            return Ok(appDbContext.Books.ToList());
        }

        [HttpDelete("{bookId}")]
        public async Task<IActionResult> DeleteBookById([FromRoute] int bookId){
            var book = await appDbContext.Books.FirstAsync(x => x.Id == bookId);
            if (book == null) {
                return NotFound();
            }
            appDbContext.Books.Remove(book);
            await appDbContext.SaveChangesAsync();
            return Ok(appDbContext.Books.ToList());
        }

        [HttpDelete("bulk")]
        public async Task<IActionResult> DeleteBooksInBulk(){
            await appDbContext.Books.Where(x=>x.Id>=3).ExecuteDeleteAsync();
            return Ok(appDbContext.Books.ToList());
        }
    }
}
