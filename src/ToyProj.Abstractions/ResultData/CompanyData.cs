using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyProj.Abstractions.ResultData
{
	public class CompanyData
	{
		public int CompanyId { get; set; }
		public string CompanyName { get; set; }
	}

	public class MovieCompanyPercentageData
	{
		public string CompanyName { get; set; }
		public int CompanyId { get; set;}
		public decimal Percentage { get; set; }
	}
}
