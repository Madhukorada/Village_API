using Digital_Village_Api.Application.DTO;
using Digital_Village_Api.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Digital_Village_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
            private readonly AuthService _authService;

            public AuthController(AuthService authService)
            {
                _authService = authService;
            }

            [HttpPost("login")]
            public async Task<IActionResult> Login(LoginRequest request)
            {
                var result = await _authService.LoginAsync(request);

                if (result == null)
                    return Unauthorized();

                return Ok(result);
            }
        }
    }
