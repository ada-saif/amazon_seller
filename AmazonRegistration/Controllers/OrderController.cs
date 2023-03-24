using AmazonRegistration.Interface;
using AmazonRegistration.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmazonRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly Iorder _order;
        public OrderController(Iorder _order)
        {
            this._order = _order;
        }
        [HttpPost]
        [Route("Getorder")]
        public IActionResult GetOrder()
        {
            return Ok(_order.GetOrder());
        }
    }
}
