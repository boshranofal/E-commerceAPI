using E_commerceAPI.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceAPI.Areas.Customer.Controllers
{
    [Route("api/[Area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _categoryServiece;

        public CategoryController(ICategoryServices categoryServiece)
        {
            _categoryServiece = categoryServiece;
        }

        [HttpGet("")]
        public IActionResult GetAll() => Ok(_categoryServiece.GetAll());


        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var categoty = _categoryServiece.GetById(id);
            if (categoty == null)
            {
                return NotFound();
            }
            return Ok(categoty);
        }
    }
}
