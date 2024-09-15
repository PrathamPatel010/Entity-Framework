using DbOperations.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DbOperations.Controllers
{
    [Route("api/languages")]
    [ApiController]
    public class LanguageController : ControllerBase {
        private readonly AppDbContext _appDbContext;

        public LanguageController(AppDbContext appDbContext) {
            _appDbContext = appDbContext;
        }

        [HttpGet("/languages/get-all")]
        public async Task<IActionResult> getAllLanguages() {
            var results = await _appDbContext.Languages.ToListAsync();
            return Ok(results);
        }
    }
}
