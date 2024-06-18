namespace ToyProj.Models
{
	public class MovieDetailViewModel
	{
		public int MovieId { get; set; }
		public string Title { get; set; }
		public string CountryCode { get; set; }
		public int ReleaseYear { get; set; }
		public string Thumbnail { get; set; }
		public string MainCastName { get; set; }
		public string CastList { get; set; }
		public decimal VotesAvg { get; set; }
		public int VotesCount { get; set; }
		public string GenreName { get; set; }
		public DateTime ReleaseDate { get; set; }
	}
}
