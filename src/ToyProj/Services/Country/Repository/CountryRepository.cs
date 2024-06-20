using Microsoft.EntityFrameworkCore;
using ToyProj.Abstractions.ResultData;
using ToyProj.Data;

namespace ToyProj.Services.Country.Repository
{
	public class CountryRepository: ICountryRepository
	{

		private DatabaseContext db;

		public CountryRepository(DatabaseContext db)
		{
			this.db = db;
		}
		public async Task<List<CountryData>> GetCountry()
		{
			var result = await db.Country.Select(x => new CountryData()
			{
				CountryName = x.CountryName,
				CountryId = x.CountryId,
				CountryIsoCode = x.CountryIsoCode
			}).ToListAsync();

			return result;
		}
	}
}
