using Microsoft.AspNetCore.Mvc;
using PixelPortalen.API.Services;
using PixelPortalen.Shared.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PixelPortalen.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService categoryService;

        public CategoryController(CategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
        {
            var categories = await categoryService.GetAllCategories();
            return Ok(categories);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] string nameOfCategory)
        {
            if (nameOfCategory is null) return BadRequest();

            var category = new Category() { Name = nameOfCategory };
            await categoryService.AddCategory(category);
            return Created("/api/category", category);
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string nameOfCategory)
        {

            var result = await categoryService.EditCategory(id, nameOfCategory);

            return Ok(result);
        }

        // WARNING CASCADE DELETE... WILL REMOVE PRODUCTS IF YOU DELETE A CATEGORY

        // DELETE api/<CategoryController>/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    await categoryService.RemoveCategory(id);
        //    return Ok();
        //}
    }
}