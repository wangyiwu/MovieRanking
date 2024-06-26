using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using ToyProj.Data;
using ToyProj.Services.Movies.Models;
using ToyProj.Services.Movies.Repository;

namespace ToyProj.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MovieController : ControllerBase
	{
		private DatabaseContext _dbContext;
		private IMovieRepository _movieRepository;

		public MovieController(DatabaseContext context, IMovieRepository movieRepository)
		{
			_dbContext = context;
			_movieRepository = movieRepository;
		}


		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await _movieRepository.DeleteAsync(id);
			if (result > 0)
			{
				return Ok(result);
			}

			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateMovieRequestModel model)
		{
			var result = await _movieRepository.CreateAsync(model);
			if(result)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest();
			}
		}


		[HttpPut]
		public async Task<IActionResult> Update([FromBody] UpdateMovieRequestModel model)
		{
			var result = await _movieRepository.UpdateAsync(model);
			if (result)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest();
			}
		}


	}
}
