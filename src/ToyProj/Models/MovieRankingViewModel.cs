 using ToyProj.Abstractions.ResultData;

namespace ToyProj.Models
{
    public class MovieRankingViewModel
    {
        public int? Skip { get; set; }
        public int? Count { get; set; } 
        public int? Year { get; set; }
        public string OrderBy { get; set; }
        public string GenreName { get; set; }
        public List<MovieRankingData> Items { get; set; }
    }

}
