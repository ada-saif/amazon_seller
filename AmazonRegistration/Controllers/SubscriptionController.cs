using AmazonSellerApi.Interface;
using AmazonSellerApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace AmazonSellerApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : Controller
    {
        private readonly IUserSubsriptionRepo _userSubsription;
       public SubscriptionController(IUserSubsriptionRepo userSubsription)
        {
            _userSubsription = userSubsription;
        }
        [HttpPost]
        [Route("AddUserSubscription")]
        public IActionResult UserRegistration(UserSubscription subscription)
        {
            return Ok(_userSubsription.SaveSubs(subscription));
        }
    }
}
