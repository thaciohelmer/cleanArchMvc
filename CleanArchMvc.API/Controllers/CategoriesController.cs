using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            var categories = await _categoryService.GetCategories();
            if (categories == null) return NotFound("Categories not found.");
            return Ok(categories);
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> GetById(int id)
        {
            var categoryDto = await _categoryService.GetById(id);
            if (categoryDto == null) return NotFound("Category not found.");
            return Ok(categoryDto);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody]CategoryDTO categoryDto)
        {
            if (categoryDto == null) return BadRequest("Invalid Data.");

            await _categoryService.Add(categoryDto);

            return new CreatedAtRouteResult("GetCategory", new { id = categoryDto.Id }, categoryDto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] CategoryDTO categoryDto)
        {
            if (id != categoryDto.Id) return BadRequest("Invalid Id.");
            if (categoryDto == null) return BadRequest("Invalid Data.");

            await _categoryService.Update(categoryDto);
            return Ok(categoryDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            var categoryDto = await _categoryService.GetById(id);
            if (categoryDto == null) return NotFound("Category not found.");

            await _categoryService.Remove(id);
            return Ok(categoryDto);
        }
    }
}
