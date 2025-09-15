using E_commerceAPI.BLL.Services.Interfaces;
using E_commerceAPI.DAL.DTO.Request;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_commerceAPI.Areas.Customer.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Customer")]
    public class CheckOutsController : ControllerBase
    {
        private readonly ICheckoutService _checkoutService;

      
        public CheckOutsController(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }
        [HttpPost("payment")]
        public async Task<IActionResult> Payment([FromBody]CheckoutRequest request)
        {
            var userId=User.FindFirstValue(ClaimTypes.NameIdentifier);

            var response = await _checkoutService.ProccessPaymentAsync(request, userId, Request);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("success/{orderId}")]
        [AllowAnonymous]
        public async Task <IActionResult> Success([FromRoute]int orderId)
        {
            var result = await _checkoutService.HendelPaymentSuccessAsync(orderId);
            return Ok(result);
        }
    }
}
