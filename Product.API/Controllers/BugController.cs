using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.API.Errors;
using Product.Infrastructure.Data;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BugController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("not-found")]
        public IActionResult GetNotFound()
        {
            var product = _context.Products.Find(50);
            if(product is null)
            {
                return NotFound(new BaseCommenResponse(404));
                //return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("server-error")]
        public IActionResult GetServerError()
        {
            var product = _context.Products.Find(100);
            product.Name = "";
            return Ok();
        }

        [HttpGet("bad-request/{id}")]
        public IActionResult GetNotFoundRequest()
        {
            return Ok();
        }

        [HttpGet("bad-request")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(); 
        }
    }
}
