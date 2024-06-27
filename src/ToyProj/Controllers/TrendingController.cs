using Microsoft.AspNetCore.Mvc;
using ToyProj.Models;
using ToyProj.Services.Company.Repository;
using ToyProj.Services.Movies.Repository;

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

            var revenueData = await movieRepository.GetTotalRevenue();

            var columChart = revenueData.GroupBy(x => x.CompanyName).Select(y => new ColumChartViewModel()
            {
                Name = y.Key,
                Data = y.Select(x => x.TotalRevenue).ToArray(),
            }).ToList();

            var pieChartViewModel = new PieChartViewModel()
            {
                Name = "Number of films released by each company",
                Series = moviePercentageData.Select(x => new PieChartData()
                {
                    Name = x.CompanyName,
                    Y = x.Percentage,
                }).ToList()
            };

            trending.PieChartViewModel = pieChartViewModel;
            trending.ColumsChartViewModel = columChart;

            return View(trending);
        }
    }
}
