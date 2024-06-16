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
        public List<MovieRankingItem> Items { get; set; }
    }

    public class MovieRankingItem
    {
        public string Title { get; set; }
        public string CountryCode { get; set; }
        public int ReleaseYear { get; set; }
        public string Thumbnail { get; set; }
        public string MainCast { get; set; }
        public List<string> CastsName { get; set; }
        public decimal VotesAvg { get; set; }
        public int VotesCount { get; set; }
        public string GenreName { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
