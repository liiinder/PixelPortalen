using Microsoft.AspNetCore.Mvc;
using PixelPortalen.API.Services;
using PixelPortalen.Shared.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PixelPortalen.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly GenreService genreService;

        public GenreController(GenreService genreService)
        {
            this.genreService = genreService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> GetAllGenres()
        {
            var genres = await genreService.GetAllGenres();
            return Ok(genres);
        }

        // POST api/<GenreController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] string nameOfGenre)
        {
            if (nameOfGenre is null) return BadRequest();

            await genreService.AddGenre(nameOfGenre);
            return Created("/api/genre", nameOfGenre);
        }

        // PUT api/<GenreController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string nameOfGenre)
        {
            var result = await genreService.EditGenre(id, nameOfGenre);

            return Ok(result);
        }

        // WARNING CASCADE DELETE... WILL REMOVE PRODUCTS WITH THE GENRE IF YOU DELETE A GENRE

        // DELETE api/<GenreController>/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    await genreService.RemoveGenre(id);
        //    return Ok();
        //}
    }
}