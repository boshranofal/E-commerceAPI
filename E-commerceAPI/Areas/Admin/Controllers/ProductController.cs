using E_commerceAPI.BLL.Services.Interfaces;
using E_commerceAPI.DAL.DTO.Request;
using KAStore.DAL.DTO.Request;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_commerceAPI.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    //[Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme,Roles = "Admin,SuperAdmin")]
    
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        [HttpGet("")]
        public IActionResult GetAll()=> Ok(_productServices.GetAllProduct(Request));
       
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm]ProductRequest productRequest)
        {
           // var userId=User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result =await _productServices.CreateFile(productRequest);
            return Ok(result);
        }
    }
}
