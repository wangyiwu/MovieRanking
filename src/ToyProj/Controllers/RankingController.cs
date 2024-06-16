using Microsoft.AspNetCore.Mvc;
using ToyProj.Models;
using ToyProj.Services.Movie.Models;
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

        public async Task<IActionResult> Index(MovieRankingViewModel model)
        {
            var requestModel = new MovieRankingRequestModel()
            {
                Count = model.Count?? 5,
                Skip = model.Skip,
                GenreName = model.GenreName,
                OrderBy = model.OrderBy,
                Year    = model.Year,
            };

            var movieRankingData = await movieRepository.GetMovieRankings(requestModel);

            var result = from movie in movieRankingData
                         group movie by movie.Title into g
                         select new MovieRankingItem
                         {
                             Title = g.Key,
                             GenreName = g.First().Title,
                             CountryCode = g.First().CountryCode,
                             ReleaseYear = g.First().ReleaseYear,
                             MainCast = g.OrderByDescending(x => x.CastOrder).Last().CastName,
                             CastsName = g.Select(x => x.CastName).ToList(),
                             VotesAvg = g.First().VotesAvg,
                             VotesCount = g.First().VotesCount,
                             ReleaseDate  = g.First().ReleaseDate,
                         };

            model.Items = result.ToList();

            return View(model);
        }

    }
}
