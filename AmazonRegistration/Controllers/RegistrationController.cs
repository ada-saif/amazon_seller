using AmazonRegistration.Interface;
using AmazonRegistration.Model;
using AmazonSellerApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmazonRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : Controller
    {

        private readonly IRegistrationInterface _registration;
        public RegistrationController(IRegistrationInterface _registration)
        {
            this._registration = _registration;
        }
        [HttpPost]
        [Route("AddUser")]
        public IActionResult UserRegistration(RegistrationModel userModel)
        {
            return Ok(_registration.UserRegistration(userModel));
        }
        [HttpPost]
        [Route("UserLogin")]
        [AllowAnonymous]
        public IActionResult UserLogin(Login login)
        {
            return Ok(_registration.UserLogin(login));
        }
        [HttpPost]
        [Route("GenerateAccessTokenbyAuthToken")]
        public IActionResult GenerateAccessToken(authModel user)
        {
            return Ok(_registration.GenerateAccessTokenByAuth(user));
        }
        [HttpPost]
        [Route("otp/VerifyOtp")]
        public IActionResult ValidateOtp(validateOtp Otp)
        {
            return Ok(_registration.ValidateOtp(Otp));
        }
        [HttpGet]
        [Route("GetbyUserId/{UserId}")]
        public IActionResult GetbyUserId(int userId)
        {
            return Ok(_registration.GetbyUserId(userId));
        }
        [HttpPost]
        [Route("UserUpdate")]
        public IActionResult UpdateUser(RegistrationModel model)
        {
            return Ok(_registration.UpdateUser(model));
        }

    }
}
