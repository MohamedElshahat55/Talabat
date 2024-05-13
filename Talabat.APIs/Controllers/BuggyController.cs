using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Repository.Data;

namespace Talabat.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly StoreDbContext _storeDbContext;

        public BuggyController(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }

        // basurl/api/Buggy/NotFound
        [HttpGet("NotFound")]
        public ActionResult GetNotFoundRequest()
        {
            var products = _storeDbContext.Products.Find(100);
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }

        [HttpGet("ServerError")]
        public ActionResult GetServerError() {
            var products = _storeDbContext.Products.Find(100);
            var ProductToReturn = products.ToString(); //Error
            return Ok(ProductToReturn);
        }

        [HttpGet("BadRequest")]
        public ActionResult GetBadRequest()
        {
            
            return BadRequest();
        }

        //Validation Error
        [HttpGet("BadRequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {

            return BadRequest();
        }
    }
}
