using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ToyProj.Abstractions.ResultData;
using ToyProj.Data;
using ToyProj.Data.Data;
using ToyProj.Services.Movies.Models;

namespace ToyProj.Services.Movies.Repository
{
	public class MovieRepository : IMovieRepository
	{
		private DatabaseContext db;

		public MovieRepository(DatabaseContext db)
		{
			this.db = db;
		}

		private const string baseRankingQuery = $@"SELECT ##LIMIT m.MovieId, m.Title as Title, c.CountryIsoCode as CountryCode, Year(m.ReleaseDate) as ReleaseYear, m.Thumbnail, MoviePersonCast.S1PersonName as MainCastName, CastList.CastList, MoviePersonCast.S1CastOrder as MainCastOrder, m.VotesAvg, m.VotesCount, g.GenreName, 
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
									 ##WHERE
									 ##ORDERBY";

		public async Task<List<int>> GetYears()
		{
			return await db.Movie.Select(x => x.ReleaseDate.Year).Distinct().ToListAsync();
		}

		public async Task<MovieRankingData> GetMovieRanking(int movieId)
		{
			var querySql = baseRankingQuery.Replace("##WHERE", $@"where m.MovieId = {movieId}").Replace("##ORDERBY", "").Replace("##LIMIT", "");

			var result = db.Database.SqlQueryRaw<MovieRankingData>(querySql).SingleOrDefault();

			return result;
		}

		public async Task<List<MovieRankingData>> GetMovieRankings(MovieRankingRequestModel query)
		{
			string orderBy = "order by " + (string.IsNullOrEmpty(query.OrderBy) ? "ReleaseYear" : query.OrderBy);

			string where = "";

			if (!string.IsNullOrEmpty(query.GenreName))
			{
				where = $@"where g.GenreName = '{query.GenreName}'";
			}

			if (!string.IsNullOrEmpty(where) && query.Year > 0)
			{
				where += $@" and Year(m.ReleaseDate) = {query.Year}";
			}
			else if (query.Year > 0)
			{
				where += $@"where Year(m.ReleaseDate) = {query.Year}";
			}

			int count = (query.Count.HasValue && query.Count > 0) ? query.Count.Value : 100;
			string limit = $@"top ({count}) ";

			var querySql = baseRankingQuery.Replace("##WHERE", where).Replace("##ORDERBY", orderBy).Replace("##LIMIT", limit);

			var result = await db.Database.SqlQueryRaw<MovieRankingData>(querySql).ToListAsync();

			return result;
		}

		public async Task<List<MovieAdminData>> MovieAdminData(MovieAdminRequestModel request)
		{
			var queryBase = $@"select 
								##LIMIT			
								m.MovieId,
								m.Title,
								m.Budget, 
								m.Homepage, 
								m.Overview, 
								m.Popularity,
								m.ReleaseDate,
								m.Revenue,
								m.MovieStatus,
								m.Tagline,
								m.VotesAvg,
								m.Runtime,
								m.VotesCount,
								m.Thumbnail,
								g.GenreId, 
								c.CountryName,
								C.CountryIsoCode as CountryCode,
								c.CountryId,
								l.LanguageId, 
								prodc.CompanyId, 
								KeywordTb.ListKeywordId 
									
						from Movie m
						join ProductionCountry pc on m.MovieId = pc.MovieId
						join Country c on pc.CountryId = c.CountryId
						join MovieGenre mg on mg.MovieId = m.MovieId
						join Genre g on mg.GenreId = g.GenreId 
						join MovieLanguages ml on ml.MovieId = m.MovieId
						join MovieCompany mc on mc.MovieId = m.MovieId
						join ProductionCompany prodc on prodc.CompanyId = mc.CompanyId
						join Language l on l.LanguageId = ml.LanguageId
						join
						(
							SELECT 
							m.MovieId as MovieId,
							STUFF(
								(SELECT ',' + CONVERT(NVARCHAR(MAX), mk.KeywordId)
								 FROM MovieKeywords mk
								 WHERE mk.MovieId = m.MovieId
								 FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 
								1, 1, '') AS ListKeywordId
						FROM 
							movie m
						GROUP BY 
							m.MovieId
						) as KeywordTb on m.MovieId = KeywordTb.MovieId
						##WHERE
						##ORDERBY
					";


			string limit = string.Empty;
			string where = string.Empty;
			string orderby = string.Empty;

			if (request.Count > 0)
			{
				limit = $@" top({request.Count}) ";
				queryBase = queryBase.Replace("##LIMIT", limit);
			}
			else
			{

			}

			queryBase = queryBase.Replace("##WHERE", where).Replace("##ORDERBY", orderby);

			var result = await db.Database.SqlQueryRaw<MovieAdminData>(queryBase).ToListAsync();

			return result;
		}

		public async Task<int> DeleteAsync(int movieId)
		{
			FormattableString sql = FormattableStringFactory.Create(@"
				BEGIN TRANSACTION;
				DELETE FROM MovieCast WHERE MovieId = {0};
				DELETE FROM MovieCrew WHERE MovieId = {0};
				DELETE FROM MovieGenre WHERE MovieId = {0};
				DELETE FROM MovieKeywords WHERE MovieId = {0};
				DELETE FROM MovieLanguages WHERE MovieId = {0};
				DELETE FROM MovieCompany WHERE MovieId = {0};
				DELETE FROM ProductionCountry WHERE MovieId = {0};
				DELETE FROM Movie WHERE MovieId = {0};
				COMMIT TRANSACTION;
				", movieId);


			var result = await db.Database.ExecuteSqlAsync(sql);

			return result;
		}


		public async Task<bool> CreateAsync(CreateMovieRequestModel movieDto)
		{

			var movie = new Movie()
			{
				Title = movieDto.Title,
				Budget = movieDto.Budget,
				Overview = movieDto.Overview,
				Homepage = movieDto.Homepage,
				Popularity = movieDto.Popularity,
				ReleaseDate = movieDto.ReleaseDate,
				Revenue	= movieDto.Revenue,
				Runtime = movieDto.Runtime,
				MovieStatus = movieDto.MovieStatus,
				Tagline = movieDto.Tagline,
				VotesAvg = movieDto.VotesAvg,
				VotesCount = movieDto.VotesCount,
				Thumbnail = "https://google.com"
			};

			var movieResult = await db.Movie.AddAsync(movie);
			await db.SaveChangesAsync();


			if (movieResult != null)
			{
				var movieId = movieResult.Entity.MovieId;

				FormattableString sqlScript = $@"

					BEGIN TRANSACTION;

					-- Insert into MovieCast
					INSERT INTO MovieCast (MovieId, PersonId, GenderId, CharacterName, CastOrder)
					VALUES ({movieId}, 1, 1, 'Character 1', 1),
						   ({movieId}, 2, 1, 'Character 2', 2),
						   ({movieId}, 3, 2, 'Character 3', 3),
						   ({movieId}, 4, 1, 'Character 4', 4),
						   ({movieId}, 5, 2, 'Character 5', 5),
						   ({movieId}, 6, 1, 'Character 6', 6),
						   ({movieId}, 7, 2, 'Character 7', 7),
						   ({movieId}, 8, 1, 'Character 8', 8),
						   ({movieId}, 9, 1, 'Character 9', 9),
						   ({movieId}, 10, 2, 'Character 10', 10);

					-- Insert into MovieCrew
					INSERT INTO MovieCrew (MovieId, PersonId, DepartmentId, Job)
					VALUES ({movieId}, 1, 1, 'Director'),
						   ({movieId}, 2, 2, 'Producer');
					
					-- Insert into MovieGenre
					INSERT INTO MovieGenre (MovieId, GenreId)
					VALUES ({movieId}, {movieDto.GenreId});

					-- Insert into MovieKeywords
					INSERT INTO MovieKeywords (MovieId, KeywordId)
					VALUES ({movieId}, 1),
						   ({movieId}, 2),
						   ({movieId}, 3);

					-- Insert into MovieLanguages
					INSERT INTO MovieLanguages (MovieId, LanguageId, LanguageRoleId)
					VALUES ({movieId}, 1, 1),
						   ({movieId}, 2, 1);

					-- Insert into MovieCompany
					INSERT INTO MovieCompany (MovieId, CompanyId)
					VALUES ({movieId}, {movieDto.CompanyId});

					-- Insert into ProductionCountry
					INSERT INTO ProductionCountry (MovieId, CountryId)
					VALUES ({movieId}, {movieDto.CompanyId});

					COMMIT TRANSACTION;";

				var result = await db.Database.ExecuteSqlAsync(sqlScript);

				return true;
			}

			return false;
		}

		public async Task<bool> UpdateAsync(UpdateMovieRequestModel movieDto)
		{
			var existed = await db.Movie.FindAsync(movieDto.MovieId);

			if(existed != null)
			{
				var movie = new Movie()
				{
					MovieId = movieDto.MovieId,
					Title = movieDto.Title,
					Budget = movieDto.Budget,
					Overview = movieDto.Overview,
					Homepage = movieDto.Homepage,
					Popularity = movieDto.Popularity,
					ReleaseDate = movieDto.ReleaseDate,
					Revenue = movieDto.Revenue,
					Runtime = movieDto.Runtime,
					MovieStatus = movieDto.MovieStatus,
					Tagline = movieDto.Tagline,
					VotesAvg = movieDto.VotesAvg,
					VotesCount = movieDto.VotesCount,
					Thumbnail = "https://google.com"
				};

				var movieResult = db.Movie.Update(movie);
				await db.SaveChangesAsync();
				return true;
			}
			return false;
		}

		public async Task<List<TotalRevenueData>> GetTotalRevenue()
		{
			var queryBase = $@"SELECT 
						pc.CompanyId,
						pc.CompanyName,
						YEAR(ReleaseDate) AS Year,
						MONTH(ReleaseDate) AS Month,
						 SUM(CAST(Revenue AS BIGINT)) AS TotalRevenue
					FROM 
						Movie m
					join MovieCompany mc on m.MovieId = mc.MovieId
					join ProductionCompany pc on pc.CompanyId = mc.CompanyId

					GROUP BY 
						pc.CompanyName,pc.CompanyId, YEAR(ReleaseDate), MONTH(ReleaseDate)
					ORDER BY 
						YEAR(ReleaseDate), MONTH(ReleaseDate);
					";

			var result = await db.Database.SqlQueryRaw<TotalRevenueData>(queryBase).ToListAsync();

			return result;
		}


	}

}



