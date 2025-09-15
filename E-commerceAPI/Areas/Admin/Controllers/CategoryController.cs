using E_commerceAPI.BLL.Services.Interfaces;
using KAStore.DAL.DTO.Request;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceAPI.Areas.Admin.Controllers
{
    [Route("api/[Area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,SuperAdmin")]
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
                if (!User.IsInRole("Admin") && !User.IsInRole("SuperAdmin"))
                    return Unauthorized();
                return NotFound();
            }
            return Ok(categoty);
        }
        [HttpPost]

        public IActionResult Create([FromBody] CategoryRequest request)
        {
            var id = _categoryServiece.Create(request);
            return CreatedAtAction(nameof(GetById), new { id }, new { message = "Ok " });
        }
        [HttpPatch("{Id}")]
        public IActionResult Update([FromRoute] int Id, [FromBody] CategoryRequest request)
        {
            var update = _categoryServiece.Update(Id, request);
            if (!User.IsInRole("Admin") && !User.IsInRole("SuperAdmin"))
                return Unauthorized();
            return update > 0 ? Ok() : NotFound();
        }
        [HttpPatch("ToggleStatus/{id}")]
        public IActionResult ToggleStatus([FromRoute] int id)
        {
            var update = _categoryServiece.ToggleStatus(id);
            if (!User.IsInRole("Admin") && !User.IsInRole("SuperAdmin"))
                return Unauthorized();
            return update ? Ok(new { message = "status toggled" }) : NotFound(new { message = "brand not found" });
        }
        [HttpDelete("{Id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _categoryServiece.Delete(id);
            if (!User.IsInRole("Admin") && !User.IsInRole("SuperAdmin"))
                return Unauthorized();
            return deleted > 0 ? Ok() : NotFound();
        }
    }
}
