 using ToyProj.Abstractions.ResultData;

namespace ToyProj.Models
{
    public class MovieRankingViewModel
    {
        public bool Top5 { get; set; } = false;
        public bool New { get; set; } = false;
        public int Year { get; set; }
        public bool All { get; set; } = false;
        public string GenreName { get; set; }
        public List<MovieRankingData> Items { get; set; }
    }

}
