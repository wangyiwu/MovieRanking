using ToyProj.Abstractions.ResultData;

namespace ToyProj.Services.Keyword.Reposigory
{
	public interface IKeywordRepository
	{
		public Task<List<KeywordData>> GetKeywords();
	}
}
