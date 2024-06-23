using Microsoft.EntityFrameworkCore;
using ToyProj.Abstractions.ResultData;
using ToyProj.Data;

namespace ToyProj.Services.Company.Repository
{
	public class CompanyRepository: ICompanyRepository
	{
		private DatabaseContext db;

		public CompanyRepository(DatabaseContext db)
		{
			this.db = db;
		}
		public async Task<List<CompanyData>> GetCompanies()
		{
			var result = await db.ProductionCompany.Select(x => new CompanyData()
			{
				CompanyId = x.CompanyId,

				CompanyName = x.CompanyName,
			}).ToListAsync();

			return result;
		}
		
		public async Task<List<MovieCompanyPercentageData>> GetMovieCompanyPercentages()
		{
			string baseQuery = $@"WITH CompanyFilmCounts AS (
										SELECT 
											mc.CompanyId,
											COUNT(mc.MovieId) AS NumberOfFilms
										FROM 
											MovieCompany mc
										GROUP BY 
											mc.CompanyId
									),
									TotalFilms AS (
										SELECT 
											COUNT(MovieId) AS TotalNumberOfFilms
										FROM 
											Movie
									)
									SELECT 
										pc.CompanyName,
										pc.CompanyId,
										CAST(cfc.NumberOfFilms AS DECIMAL(10, 2)) / tf.TotalNumberOfFilms * 100 AS Percentage
									FROM 
										CompanyFilmCounts cfc
									JOIN 
										ProductionCompany pc ON cfc.CompanyId = pc.CompanyId,
										TotalFilms tf
									ORDER BY 
										Percentage DESC;";


			var result = db.Database.SqlQueryRaw<MovieCompanyPercentageData>(baseQuery);
			
			return await result.ToListAsync();
		}
	}
}
