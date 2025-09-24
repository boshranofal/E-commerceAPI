using E_commerceAPI.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceAPI.Areas.Customer.Controllers
{
    [Route("api/[Area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Customer")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandServices _brandServices;

        public BrandController(IBrandServices brandServices)
        {
            _brandServices = brandServices;
        }
        [HttpGet("")]
        public IActionResult GetAll() => Ok(_brandServices.GetAll());


        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var brand = _brandServices.GetById(id);
            if (brand == null)
            {
                return NotFound();
            }
            return Ok(brand);
        }
    }
    }
