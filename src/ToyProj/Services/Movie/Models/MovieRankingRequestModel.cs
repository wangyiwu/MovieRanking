namespace ToyProj.Services.Movie.Models
{
    public class MovieRankingRequestModel
    {
        public int? Skip { get; set; }
        public int? Count { get; set; }
        public int? Year { get; set; }
        public string OrderBy { get; set; }
        public string GenreName {  get; set; } 
    }
}
