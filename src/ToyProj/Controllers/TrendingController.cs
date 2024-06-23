using Microsoft.AspNetCore.Mvc;
using ToyProj.Models;
using ToyProj.Services.Company.Repository;
using ToyProj.Services.Movie.Repository;

namespace ToyProj.Controllers
{
    public class TrendingController : Controller
    {
        private IMovieRepository movieRepository;
        private ICompanyRepository companyRepository;

        public TrendingController(IMovieRepository movieRepository, ICompanyRepository companyRepository)
        {
            this.movieRepository = movieRepository;
            this.companyRepository = companyRepository; 

        }
		public async Task<IActionResult> Index()
        {
            var trending = new TrendingViewModel();

            var moviePercentageData = await companyRepository.GetMovieCompanyPercentages();

            var pieChartViewModel = new PieChartViewModel()
            {
                Name = "Movie Percentage",
                Series = moviePercentageData.Select(x => new PieChartData()
                {
                    Name = x.CompanyName,
                    Y = x.Percentage,
                }).ToList()
            };

            trending.PieChartViewModel = pieChartViewModel;

            return View(trending);
        }
    }
}
