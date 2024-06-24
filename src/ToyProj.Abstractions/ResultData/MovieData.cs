using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyProj.Abstractions.ResultData
{
	public class MovieData
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
	}
}
