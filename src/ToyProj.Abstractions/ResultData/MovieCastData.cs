using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyProj.Abstractions.ResultData
{
	public class MovieCastData
	{
		public int MovieId { get; set; }
		public int PersonId { get; set; }
		public int GenderId { get; set; }
		public string CharacterName { get; set; }
		public int CastOrder {  get; set; }
	}
}
