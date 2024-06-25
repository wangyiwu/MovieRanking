using Microsoft.AspNetCore.Mvc;
using ToyProj.Models;
using ToyProj.Services.Movie.Models;
using ToyProj.Services.Movie.Repository;
using ToyProj.Services.MovieCast.Models;
using ToyProj.Services.MovieCast.Repository;

namespace ToyProj.Controllers
{
    public class AdminController : Controller
    {
        private IMovieCastRepository movieCastRepository;
        private IMovieRepository movieRepository;


        public AdminController(
			IMovieCastRepository movieCastRepository,
			IMovieRepository movieRepository
			) { 

            this.movieCastRepository = movieCastRepository;
            this.movieRepository = movieRepository;
        }

        public async Task<IActionResult> Index()
        {
            var adminDataRequest = new MovieAdminRequestModel()
            {

            };

            var movieCastRequest = new GetMovieCastRequestModel();

            var adminMovieList = await movieRepository.MovieAdminData(adminDataRequest);

            var movieCastList = await movieCastRepository.GetMovieCastDatas(movieCastRequest);

            var adminDateItem = adminMovieList.Select(x =>
            
                new MovieAdminItem()
                {
                    MovieId = x.MovieId,
                    Title = x.Title,
                    MovieStatus = x.MovieStatus,
                    CompanyId = x.CompanyId,    
                    CountryId = x.CountryId,
                    Overview = x.Overview,
                    Homepage = x.Homepage,
                    Budget = x.Budget,
                    Popularity = x.Popularity,
                    ReleaseDate = x.ReleaseDate,
                    Revenue = x.Revenue,
                    VotesAvg = x.VotesAvg,
                    VotesCount = x.VotesCount,
                    LanguagueId = x.LanguageId,
                    Runtime = x.Runtime,
                    Tagline = x.Tagline,
                    GenreId = x.GenreId,
                    DepartmentId = 1,
                    KeywordId = x.ListKeywordId.Split(',').Select(x => int.Parse(x)).ToList(),
                    MovieCastModels = movieCastList.Where(a => a.MovieId == x.MovieId).Select(x => new MovieCastModel() {
                        
                    }).ToList()
                }
            );

            var viewModel = new MovieAdminViewModel()
            {
                ListMovie = adminDateItem.ToList()
                
            };

			return View(viewModel);
        }
        
    }
}
