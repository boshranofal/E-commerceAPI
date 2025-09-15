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
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme, Roles = "Customer")]

    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddCart(CartRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    
            var result = await _cartService.AddToCartAsync(request, userId);
            if (!result)
            {
                return NotFound($"Failed to add to cart. Check if productId {request.productId} exists and is valid.");
            }
            return Ok();
        }
        [HttpGet("summary")]
        public async Task<IActionResult> GetUserCart()
        {
            var userId= User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result= _cartService.CartSummaryResponcseAsync(userId);
            return Ok(result);
        }
       
    }
}
