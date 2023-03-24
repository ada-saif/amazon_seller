using AmazonRegistration.Interface;
using AmazonRegistration.Model;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace AmazonRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class productController : Controller
    {

        private readonly IProduct _product;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _config;

        public productController(IProduct _product, IConfiguration _config, IHttpContextAccessor _httpContextAccessor)
        {
            this._product = _product;
            this._httpContextAccessor = _httpContextAccessor;
            this._config = _config;
        }
        [HttpPost]
        [Route("GetProduct")]

        public IActionResult GetProduct()
        {
            return Ok(_product.GetProduct());
        }
    }
}



