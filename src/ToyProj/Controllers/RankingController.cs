using Microsoft.AspNetCore.Mvc;
using ToyProj.Models;
using ToyProj.Services.Genre.Repository;
using ToyProj.Services.Movie.Models;
using ToyProj.Services.Movie.Repository;

namespace ToyProj.Controllers
{
    public class RankingController : Controller
    {
        private IMovieRepository movieRepository;
        private IGenreRepository genreRepository;

        public RankingController(IMovieRepository movieRepository,
            IGenreRepository genreRepository)
        {
            this.movieRepository = movieRepository;
            this.genreRepository = genreRepository;
        }

        public async Task<IActionResult> Index(MovieRankingViewModel model)
        {
            var genre = await genreRepository.GetGenre();

            ViewBag.Genre = genre;

            var requestModel = new MovieRankingRequestModel()
            {
                Count = model.Top5 == true ? 5 : 10,
                Skip = model.Skip,
                GenreName = model.GenreName,
                OrderBy = model.OrderBy,
                Year = model.Year,
            };

            var movieRankingData = await movieRepository.GetMovieRankings(requestModel);

            model.Items = movieRankingData.ToList();

            return View(model);
        }

        public async Task<IActionResult> MovieDetail()
        {
            return View();
        }

    }
}
