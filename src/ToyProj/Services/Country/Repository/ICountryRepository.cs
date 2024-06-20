using ToyProj.Abstractions.ResultData;

namespace ToyProj.Services.Country.Repository
{
	public interface ICountryRepository
	{
		public Task<List<CountryData>> GetCountry();
	}
}
