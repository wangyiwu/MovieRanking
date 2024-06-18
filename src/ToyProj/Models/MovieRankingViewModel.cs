 using ToyProj.Abstractions.ResultData;

namespace ToyProj.Models
{
    public class MovieRankingViewModel
    {
        public int Skip { get; set; }
        public bool Top5 { get; set; } = false;
        public bool New { get; set; } = false;
        public int Count { get; set; } 
        public int Year { get; set; }
        public bool All { get; set; } = false;
        public string OrderBy { get; set; }
        public string GenreName { get; set; }
        public List<MovieRankingData> Items { get; set; }
    }

}
