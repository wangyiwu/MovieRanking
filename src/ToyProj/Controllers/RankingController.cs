using Microsoft.AspNetCore.Mvc;
using ToyProj.Services.Movie.Model;
using ToyProj.Services.Movie.Repository;

namespace ToyProj.Controllers
{
    public class RankingController : Controller
    {
        private IMovieRepository movieRepository;

        public RankingController(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        public async Task<IActionResult> Index()
        {
            var model = new MovieRankingRequestModel()
            {
                OrderBy = "ReleaseDate"
            };

            var result = await movieRepository.GetMovieRankings(model);

            return Json(result);
        }

    }
}
