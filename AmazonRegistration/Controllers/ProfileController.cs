using AmazonRegistration.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmazonRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProfileController : Controller
    {
        private readonly IProfile _profile;
        public ProfileController(IProfile profile)
        {
            _profile = profile;
        }

        [HttpGet]
        [Route("GetUserCount/{UserId}")]
        public IActionResult GetUserCount( int UserId)
        {
            return Ok(_profile.GetUserCount(UserId));
        }

        [HttpGet]
        [Route("GetUserProfile/{UserId}")]
        public IActionResult GetUserDetail(int UserId)
        {
            return Ok(_profile.GetUserDetail(UserId));
        }
    }
}
