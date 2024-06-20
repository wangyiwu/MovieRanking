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
	}
}
