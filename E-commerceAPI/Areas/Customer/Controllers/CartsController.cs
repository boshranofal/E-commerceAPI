using E_commerceAPI.BLL.Services.Interfaces;
using E_commerceAPI.DAL.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_commerceAPI.Areas.Customer.Controllers
{
        [Route("api/[area]/[controller]")]
        [ApiController]
        [Area("Customer")]
       [Authorize(Roles = "Customer")]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpPost("AddToCart")]
        public IActionResult AddCart(CartRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    
            var result = _cartService.AddToCart(request, userId);
            if (!result)
            {
                return NotFound($"Failed to add to cart. Check if productId {request.productId} exists and is valid.");
            }
            return Ok();
        }
        [HttpGet("summary")]
        public IActionResult GetUserCart()
        {
            var userId= User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result= _cartService.CartSummaryResponcse(userId);
            return Ok(result);
        }
       // [Authorize]
        [HttpGet("test-claims")]
        public IActionResult TestClaims()
        {
            foreach (var claim in User.Claims)
            {
                Console.WriteLine($"{claim.Type} => {claim.Value}");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Ok(new { userId, claims = User.Claims.Select(c => new { c.Type, c.Value }) });
        }
    }
}
