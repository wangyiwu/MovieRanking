using Microsoft.AspNetCore.Mvc;
using ToyProj.Models;
using ToyProj.Services.Genre.Repository;
using ToyProj.Services.Movies.Models;
using ToyProj.Services.Movies.Repository;

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
            var genre = await genreRepository.GetGenres();
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

            model.Items = DistinctBy(movieRankingData, x => x.MovieId).ToList();

            return View(model);
        }

		public static IEnumerable<TSource> DistinctBy<TSource, TKey>
			(IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			HashSet<TKey> seenKeys = new HashSet<TKey>();
			foreach (TSource element in source)
			{
				if (seenKeys.Add(keySelector(element)))
				{
					yield return element;
				}
			}
		}

		[HttpGet]
		public RedirectToActionResult Redirect(string toAction, string backQueryParam)
		{
			var queryDict = ParseQueryString(backQueryParam);

			var model = ConvertToModel<MovieRankingViewModel>(queryDict);

			return RedirectToAction(toAction, "Ranking", model);
		}

		private Dictionary<string, string> ParseQueryString(string queryString)
		{
			var queryDictionary = System.Web.HttpUtility.ParseQueryString(queryString);
			return queryDictionary.AllKeys.ToDictionary(k => k, k => queryDictionary[k]);
		}

		private T ConvertToModel<T>(Dictionary<string, string> dictionary) where T : new()
		{
			var obj = new T();
			var objType = typeof(T);

			foreach (var kvp in dictionary)
			{
				var property = objType.GetProperty(kvp.Key);
				if (property != null && property.CanWrite)
				{
					var convertedValue = Convert.ChangeType(kvp.Value, property.PropertyType);
					property.SetValue(obj, convertedValue);
				}
			}

			return obj;
		}

		public async Task<IActionResult> MovieDetail(int movieId, string backQueryParam)
		{
            var movie = await movieRepository.GetMovieRanking(movieId);

            var result = new MovieDetailViewModel()
            {
                Title = movie.Title,
                BackQueryParam = backQueryParam,
                CastList = movie.CastList,
                MainCastName = movie.MainCastName,
                VotesAvg = movie.VotesAvg,
            };

            return View(result);
        }



    }
}
