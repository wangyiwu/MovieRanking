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

            var columChartGrouped = revenueData.GroupBy(x => new
            {
                x.CompanyName,
                x.Year,
            });

            var columChart = new List<ColumChartViewModel>();

            foreach (var item in columChartGrouped)
            {
                var chartItem = new ColumChartViewModel();
                chartItem.Data = new long[12] {0,0,0,0,0,0,0,0,0,0,0,0};
                chartItem.Name = $@"{item.Key.CompanyName} {item.Key.Year}";

                foreach(var i in item)
                {
                    int indexOfData = i.Month;
                    chartItem.Data[indexOfData-1] = i.TotalRevenue;
				}

                columChart.Add(chartItem);
			}

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
