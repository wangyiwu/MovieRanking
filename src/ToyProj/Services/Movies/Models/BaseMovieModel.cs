namespace ToyProj.Services.Movies.Models
{
	public class BaseMovieModel
	{
		public string Title { get; set; }
		public int Budget { get; set; }
		public string Homepage { get; set; }
		public string? Thumbnail { get; set; }
		public string Overview { get; set; }
		public decimal Popularity { get; set; }
		public DateTime ReleaseDate { get; set; }
		public int Revenue { get; set; }
		public int Runtime { get; set; }
		public string MovieStatus { get; set; }
		public string Tagline { get; set; }
		public decimal VotesAvg { get; set; }
		public int VotesCount { get; set; }
		public string KeywordId { get; set; }
		public int CountryId { get; set; }
		public int GenreId { get; set; }
		public int LanguagueId { get; set; }
		public int CompanyId { get; set; }
		public int DepartmentId { get; set; }
	}
}
