using E_commerceAPI.BLL.Services.Classes;
using E_commerceAPI.BLL.Services.Interfaces;
using E_commerceAPI.DAL.DTO.Request;
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
                if (!User.IsInRole("Admin") && !User.IsInRole("SuperAdmin"))
                    return Unauthorized();
                return NotFound();
            }
            return Ok(brand);
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create([FromForm] BrandRequest request)
        {
            var result = await _brandServices.CreateFile(request);
            return Ok(result);
        }

        [HttpPatch("{Id}")]
        public IActionResult Update([FromRoute] int Id, [FromBody] BrandRequest request)
        {
            var update = _brandServices.Update(Id, request);
            if (!User.IsInRole("Admin") && !User.IsInRole("SuperAdmin"))
                return Unauthorized();
            return update > 0 ? Ok() : NotFound();
        }

        [HttpPatch("ToggleStatus/{id}")]
        public IActionResult ToggleStatus([FromRoute] int id)
        {
            var update = _brandServices.ToggleStatus(id);
            if (!User.IsInRole("Admin") && !User.IsInRole("SuperAdmin"))
                return Unauthorized();
            return update ? Ok(new { message = "status toggled" }) : NotFound(new { message = "brand not found" });
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _brandServices.Delete(id);
            if (!User.IsInRole("Admin") && !User.IsInRole("SuperAdmin"))
                return Unauthorized();
            return deleted > 0 ? Ok() : NotFound();
        }
    }
}
