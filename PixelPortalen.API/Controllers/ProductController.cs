using Microsoft.AspNetCore.Mvc;
using PixelPortalen.API.Services;
using PixelPortalen.Shared.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PixelPortalen.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService productService;
        private readonly IWebHostEnvironment _env;

        public ProductController(ProductService productService, IWebHostEnvironment env)
        {
            this.productService = productService;
            this._env = env;
        }

        // GET: api/<ProductController>
        [HttpGet("InStock")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProductsInStock()
        {
            var products = await productService.GetProductsInStock();
            return Ok(products);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var products = await productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("onPage/{pageNumber}/{pageSize}")]
        public async Task<ActionResult<PagedResult<Product>>> GetProductsOnPage(int pageNumber = 1, int pageSize = 10)
        {
            var result = await productService.GetPagedProducts(pageNumber, pageSize);
            return Ok(result);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var product = await productService.GetById(id);
            if (product is null) return NotFound($"No product found with id: {id}");
            return Ok(product);
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Product product)
        {
            if (product is null) return BadRequest();

            await productService.AddProduct(product);
            return Created("/api/product", product);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Product product)
        {
            var result = await productService.EditProduct(id, product);

            return Ok(result);
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await productService.RemoveProduct(id);
            return Ok();
        }
    }
}