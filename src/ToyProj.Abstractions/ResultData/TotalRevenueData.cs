using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyProj.Abstractions.ResultData
{
	public class TotalRevenueData
	{
		public int CompanyId { get; set; }
		public string CompanyName { get; set; }
		public int Year { get; set; }
		public int Month { get; set; }
		public long TotalRevenue { get; set; }
	}
}
