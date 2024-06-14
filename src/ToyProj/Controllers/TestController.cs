using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToyProj.Data;
using ToyProj.Data.Data;

namespace ToyProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public TestController(DatabaseContext context) 
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetMovie()
        {
           
            var query = _context.Database.SqlQueryRaw<Country>(
                "select * from Country"
             );

            var result = query.ToList();

            return Ok(result);
        }
    }
}
