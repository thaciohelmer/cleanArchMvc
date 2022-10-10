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
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var products = await _productService.GetProducts();
            if (products == null) return NotFound("Products not found");
            return Ok(products);
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> GetById(int id)
        {
            var product = await _productService.GetById(id);
            if (product == null) return NotFound("Product not found.");
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ProductDTO productDto)
        {
            if (productDto == null) return BadRequest("Data invalid.");

            await _productService.Add(productDto);
            return new CreatedAtRouteResult("GetProduct", new {id = productDto.Id}, productDto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProductDTO>> Update(int id, [FromBody] ProductDTO productDto)
        {
            if (id != productDto.Id) return BadRequest("Invalid Id.");

            if (productDto == null) return BadRequest("Invalid Data.");

            await _productService.Update(productDto);
            return Ok(productDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            var productDto = await _productService.GetById(id);
            if (productDto == null) return NotFound("Product not found.");

            await _productService.Remove(id);
            return Ok(productDto);
        }

    }
}
