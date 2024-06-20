using ToyProj.Abstractions.ResultData;
using ToyProj.Services.MovieCast.Models;

namespace ToyProj.Services.MovieCast.Repository
{
	public interface IMovieCastRepository
	{
		public Task<List<MovieCastData>> GetMovieCastDatas(GetMovieCastRequestModel request);

	}
}
