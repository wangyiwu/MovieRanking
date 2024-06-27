using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyProj.Abstractions.ResultData;
using ToyProj.Data.Data;
using ToyProj.Services.Movies.Models;

namespace ToyProj.Services.Movies.Repository
{
    public interface IMovieRepository
    {
        public Task<List<MovieRankingData>> GetMovieRankings(MovieRankingRequestModel query);
		public Task<MovieRankingData> GetMovieRanking(int movieId);
		public Task<List<int>> GetYears();
		public Task<List<MovieAdminData>> MovieAdminData(MovieAdminRequestModel request);

		public Task<int> DeleteAsync(int movieId);

		public Task<bool> CreateAsync(CreateMovieRequestModel movie);
		public Task<bool> UpdateAsync(UpdateMovieRequestModel movie);

		public Task<List<TotalRevenueData>> GetTotalRevenue();


	}
}
