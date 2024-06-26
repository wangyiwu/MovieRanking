using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using ToyProj.Data;

namespace ToyProj.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MovieController : ControllerBase
	{
		private DatabaseContext _dbContext;

		public MovieController(DatabaseContext context)
		{
			_dbContext = context;
		}


		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var existed = await _dbContext.Movie.FindAsync(id);
			if (existed != null) 
			{
				var query = "BEGIN TRANSACTION;\r\n\r\n-- Delete related records from MovieCast\r\nDELETE FROM MovieCast WHERE MovieId = {MOVIE_ID};\r\n\r\n-- Delete related records from MovieCrew\r\nDELETE FROM MovieCrew WHERE MovieId = {MOVIE_ID};\r\n\r\n-- Delete related records from MovieGenre\r\nDELETE FROM MovieGenre WHERE MovieId = {MOVIE_ID};\r\n\r\n-- Delete related records from MovieKeywords\r\nDELETE FROM MovieKeywords WHERE MovieId = {MOVIE_ID};\r\n\r\n-- Delete related records from MovieLanguages\r\nDELETE FROM MovieLanguages WHERE MovieId = {MOVIE_ID};\r\n\r\n-- Delete related records from MovieCompany\r\nDELETE FROM MovieCompany WHERE MovieId = {MOVIE_ID};\r\n\r\n-- Delete related records from ProductionCountry\r\nDELETE FROM ProductionCountry WHERE MovieId = {MOVIE_ID};\r\n\r\n-- Finally, delete the movie record\r\nDELETE FROM Movie WHERE MovieId = {MOVIE_ID};\r\n\r\nCOMMIT TRANSACTION;"
			}

			return NotFound();
		}

	}
}
