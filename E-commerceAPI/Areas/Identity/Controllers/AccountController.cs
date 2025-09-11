using E_commerceAPI.BLL.Services.Interfaces;
using E_commerceAPI.DAL.DTO.Request;
using E_commerceAPI.DAL.DTO.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceAPI.Areas.Identity.Controllers
{
    [Route("api/[Area]/[controller]")]
    [ApiController]
    [Area("Identity")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost("Register")]

        public async Task<ActionResult<UserResponce>>Register(RegisterRequest request)
        {
            var result=await _authenticationService.RegisterAsync(request,Request);
            return Ok(result);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserResponce>> Login(LoginRequest request)
        {
            var result=await _authenticationService.LoginAsync(request);
            return Ok(result);
        }


        [HttpGet("ConfirmEmail")]

        public async Task<ActionResult<string>> ConfirmEmail([FromQuery]string token, [FromQuery]string userId)
        {
            var result = await _authenticationService.ConfirmEmail(token,userId);
            return Ok(result);
        }
        [HttpPost("ForgetPassword")]
        public async Task<ActionResult<string>> ForgetPassword([FromBody] ForgetPasswordRequest request)
        {
            var result = await _authenticationService.ForgetPassword(request);
            return Ok(result);
        }
    }
}
 