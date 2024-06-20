using ToyProj.Abstractions.ResultData;

namespace ToyProj.Services.Company.Repository
{
	public interface ICompanyRepository
	{
		public Task<List<CompanyData>> GetCompanies();
	}
}
