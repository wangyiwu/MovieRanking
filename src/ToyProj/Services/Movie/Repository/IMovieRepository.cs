using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyProj.Abstractions.ResultData;
using ToyProj.Services.Movie.Models;

namespace ToyProj.Services.Movie.Repository
{
    public interface IMovieRepository
    {
        public Task<List<MovieRankingData>> GetMovieRankings(MovieRankingRequestModel query);
    }
}
