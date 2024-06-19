using Microsoft.AspNetCore.Mvc;
using ToyProj.Abstractions.ResultData;
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
            var listYear = await movieRepository.GetYears();

            ViewBag.Genre = genre;
            ViewBag.ListYear = listYear;

            var requestModel = new MovieRankingRequestModel()
            {
                Count = model.Top5 ? 5 : model.All ? int.MaxValue : 100,
                GenreName = model.GenreName,
                Year = model.Year,
                OrderBy = model.New ? "ReleaseDate" : "ReleaseYear"
            };

            var movieRankingData = await movieRepository.GetMovieRankings(requestModel);

            model.Items = movieRankingData.ToList();

            return View(model);
        }

        public async Task<IActionResult> MovieDetail(int movieId)
		{
            var request = new MovieRankingRequestModel()
            {

            };

			var movie =( await movieRepository.GetMovieRankings(request)).Find(x => x.MovieId == movieId);

            var result = new MovieDetailViewModel()
            {
                Title = movie.Title
            };

            return View(result);
        }

    }
}
