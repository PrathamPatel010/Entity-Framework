using DbOperations.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DbOperations.Controllers
{
    [Route("api/currencies")]
    [ApiController]
    public class CurrencyController : ControllerBase {
        private readonly AppDbContext _appDbContext;

        public CurrencyController(AppDbContext appDbContext) {
            _appDbContext = appDbContext;
        }

        //[HttpGet("")]
        //public IActionResult GetAllCurrencies() {
        //    //Method-1
        //    //var results = _appDbContext.Currencies.ToList();
        //    //return Ok(results);

        //    //Method-2
        //    var results = from currencies in _appDbContext.Currencies
        //                  select currencies;
        //    return Ok(results.ToList());
        //}

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllCurrencies() {
            var results = await _appDbContext.Currencies.ToListAsync();
            return Ok(results);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCurrencyByIdAsync([FromRoute] int id) {
            var result = await _appDbContext.Currencies.FindAsync(id);
            return Ok(result);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetCurrencyByName([FromRoute] string name) {
            //var result = await _appDbContext.Currencies.Where(x=>x.Title== name).FirstOrDefaultAsync();

            //Query Optimization
            var result = await _appDbContext.Currencies.FirstOrDefaultAsync(x=>x.Title==name);
            return Ok(result);
        }

        [HttpGet("multiple/{name}")]
        public async Task<IActionResult> GetCurrenciesByMultiple([FromRoute]string name, [FromQuery]string? description) {
            // for getting multiple just use where clause and use ToList. Everything else is same.
            var result = await _appDbContext.Currencies.FirstOrDefaultAsync(x => 
                x.Title == name &&
                (string.IsNullOrEmpty(description) || x.Description ==description));
            return Ok(result);
        }

        [HttpPost("all")]
        public async Task<IActionResult> GetCurrenciesByIdsAsync([FromBody] List<int>ids){
            //var results = await _appDbContext.Currencies.Where(x => ids.Contains(x.Id)).ToListAsync();
            
            // for searching for specific columns only
            var results = await _appDbContext.Currencies.
                Where(x=>ids.Contains(x.Id)).
                Select(x=>new Currency() {
                    Id=x.Id,Title=x.Title
                }).
                ToListAsync();
            return Ok(results);
        }
    }
}
