using Microsoft.EntityFrameworkCore;
using ToyProj.Abstractions.ResultData;
using ToyProj.Data;

namespace ToyProj.Services.Keyword.Reposigory
{
	public class KeywordRepository: IKeywordRepository
	{
		private DatabaseContext db;

		public KeywordRepository(DatabaseContext db)
		{
			this.db = db;
		}


		public async Task<List<KeywordData>> GetKeywords()
		{
			return await db.Keyword.Select(x => new KeywordData()
			{
				KeywordId = x.KeywordId,
				KeywordName = x.KeywordName,
			}).ToListAsync();
		}

	}
}
