namespace ToyProj.Models
{
	public class MovieAdminViewModel
	{
		public List<MovieAdminItem> ListMovie { get; set; }
	}

	public class MovieAdminItem
	{
		public int MovieId { get; set; }
		public string Title { get; set; }
		public int Budget { get; set; }
		public string Homepage { get; set; }
		public string Overview { get; set; }
		public decimal Popularity { get; set; }

		public DateTime ReleaseDate { get; set; }
		public int Revenue { get; set; }
		public int Runtime { get; set; }
		public string MovieStatus { get; set; }
		public string Tagline { get; set; }
		public decimal VotesAvg { get; set; }
		public int VotesCount { get; set; }

		public List<int> KeywordId { get; set; }
		public int CountryId { get; set; }
		public int GenreId { get; set; }
		public int LanguagueId { get; set; }
		public int CompanyId { get; set; }
		public int DepartmentId { get; set; }
		public List<MovieCastModel> MovieCastModels { get; set; }
	}

	public class MovieCastModel
	{
		public int MovieId { get; set; }
		public int PersonId { get; set; }
		public int GenderId { get; set; }
		public string CharacterName { get; set; }
		public int CastOrder { get; set; }
	}
}
