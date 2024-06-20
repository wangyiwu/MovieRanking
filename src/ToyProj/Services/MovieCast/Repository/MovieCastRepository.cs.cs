using Microsoft.EntityFrameworkCore;
using ToyProj.Abstractions.ResultData;
using ToyProj.Data;
using ToyProj.Services.MovieCast.Models;

namespace ToyProj.Services.MovieCast.Repository
{
	public class MovieCastRepository: IMovieCastRepository
	{
		private DatabaseContext db;

		public MovieCastRepository(DatabaseContext db)
		{
			this.db = db;
		}
		public async Task<List<MovieCastData>> GetMovieCastDatas(GetMovieCastRequestModel request)
		{
			var baseQuery = $@"select m.MovieId, p.PersonId, g.GenderId, mc.CharacterName, mc.CastOrder from Movie m
								join MovieCast mc on m.MovieId = mc.MovieId
								join Person p on p.PersonId = mc.PersonId
								join Gender g on mc.GenderId = g.GenderId";

			return await db.Database.SqlQueryRaw<MovieCastData>(baseQuery).ToListAsync();
		}
	}
}
