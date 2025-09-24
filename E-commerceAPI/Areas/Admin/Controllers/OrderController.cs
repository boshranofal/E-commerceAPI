using E_commerceAPI.BLL.Services.Interfaces;
using E_commerceAPI.DAL.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceAPI.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,SuperAdmin")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService )
        {
            _orderService = orderService;
        }
        [HttpGet("Status/{status}")]
        public async Task<IActionResult> GetByStatus( StatusOrderEnum status)
        {
            var order = await _orderService.GetByStatus(status);
            return Ok(order);
        }
        [HttpPatch("ChangeStatus/{orderId}")]
        public async Task<IActionResult>ChangeStatus(string orderId, StatusOrderEnum newStatus)
        {
            var isChanged = await _orderService.ChangeStatusAsync(orderId, newStatus);
            if (!isChanged)
                return BadRequest("Status not changed");
            return Ok("Status changed successfully");
        }
    }
}
