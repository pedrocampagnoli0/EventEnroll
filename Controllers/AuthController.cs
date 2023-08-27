using EventEnroll.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using EventEnroll.Dtos.Auth;

namespace EventEnroll.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(IAuthRepository authRepo, UserManager<IdentityUser> userManager)
        {
            _authRepo = authRepo;
            _userManager = userManager;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<string>> Register(RegisterUserDto user)
        {
            var response = await _authRepo.Register(new RegisterUserDto { UserName = user.UserName, Password = user.Password});
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<int>>> Login(LoginUserDto request)
        {
            var response = await _authRepo.Login(request.UserName, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
