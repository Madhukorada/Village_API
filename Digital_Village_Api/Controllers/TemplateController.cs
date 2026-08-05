using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Digital_Village_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateController : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        public IActionResult AdminData()
        {
            return Ok("Admin Data");
        }

        [Authorize(Roles = "User")]
        [HttpGet("user")]
        public IActionResult UserData()
        {
            return Ok("User Data");
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {
            return Ok("Dashboard");
        }
        [Authorize]
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Authentication Success");
        }
    }
}
