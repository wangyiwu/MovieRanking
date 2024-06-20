using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyProj.Abstractions.ResultData;
using ToyProj.Data;
using ToyProj.Services.Movie.Models;

namespace ToyProj.Services.Movie.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private DatabaseContext db;

        public MovieRepository(DatabaseContext db)
        {
            this.db = db;
        }

		public async Task<List<int>> GetYears()
		{
			return await db.Movie.Select(x => x.ReleaseDate.Year).Distinct().ToListAsync();
		}

		public async Task<List<MovieRankingData>> GetMovieRankings(MovieRankingRequestModel query)
        {
            string orderBy = string.IsNullOrEmpty(query.OrderBy) ? "ReleaseYear" : query.OrderBy;

            string where = "";

            if (!string.IsNullOrEmpty(query.GenreName))
            {
                where = $@"where g.GenreName = '{query.GenreName}'";
            }

			if (!string.IsNullOrEmpty(where) && query.Year > 0)
			{
				where += $@" and Year(m.ReleaseDate) = {query.Year}";
			}
			else if(query.Year > 0)
			{
				where += $@"where Year(m.ReleaseDate) = {query.Year}";
			}

            int count = (query.Count.HasValue && query.Count > 0)  ? query.Count.Value : 100;

			var querySql = $@"SELECT top ({count}) m.MovieId, m.Title as Title, c.CountryIsoCode as CountryCode, Year(m.ReleaseDate) as ReleaseYear, m.Thumbnail, MoviePersonCast.S1PersonName as MainCastName, CastList.CastList, MoviePersonCast.S1CastOrder as MainCastOrder, m.VotesAvg, m.VotesCount, g.GenreName, 
                                m.ReleaseDate as ReleaseDate
                                  FROM Movie m 
                                  join ProductionCountry pc on m.MovieId = pc.MovieId
                                  join Country c on pc.CountryId = c.CountryId
                                  join MovieGenre mg on mg.MovieId = m.MovieId
                                  join Genre g on mg.GenreId = g.GenreId 
								  -- get main cast name, cast order
								  join 
								  (
									select s1_m.MovieId as S1MovieId, s1_p.PersonName as S1PersonName, s1_mc.CastOrder as S1CastORder from Movie s1_m join
												MovieCast s1_mc on s1_mc.MovieId = s1_m.MovieId 
												join
												Person s1_p on s1_p.PersonId = s1_mc.PersonId
												where s1_mc.CastOrder = (select Min(CastOrder) from MovieCast where MovieId = s1_mc.MovieId)
								  ) as MoviePersonCast on m.MovieId =  MoviePersonCast.S1MovieId
								  join
								  (
									SELECT
										m.MovieId as MovieId,
										STRING_AGG(P.PersonName, ', ') AS CastList
									FROM
										Movie M
										INNER JOIN MovieCast MCMain ON M.MovieId = MCMain.MovieId
										INNER JOIN Person PMain ON MCMain.PersonId = PMain.PersonId
										INNER JOIN MovieCast MC ON M.MovieId = MC.MovieId
										INNER JOIN Person P ON MC.PersonId = P.PersonId
									GROUP BY
										M.Title, PMain.PersonName, M.MovieId   
								  ) as CastList  on CastList.MovieId = m.MovieId
									 {where}
                                  order by {orderBy} desc";

			var result = await db.Database.SqlQueryRaw<MovieRankingData>(querySql).ToListAsync();

            return result;
        }
    }


}
