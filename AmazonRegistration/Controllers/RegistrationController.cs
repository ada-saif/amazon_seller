using AmazonRegistration.Interface;
using AmazonRegistration.Model;
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
        //[HttpPost]
        //[Route("AddUser")]
        //public IActionResult UserRegistration(RegistrationModel userModel)
        //{
        //    return Ok(_registration.UserRegistration(userModel));
        //}
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
            return Ok(_registration.GenerateAccessTokenss(user));
        }
       
    }
}
