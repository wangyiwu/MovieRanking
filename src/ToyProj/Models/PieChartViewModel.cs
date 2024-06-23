using System.Text.Json.Serialization;

namespace ToyProj.Models
{
	public class PieChartViewModel
	{
		public List<PieChartData> Series { get; set; }
		public string Name { get; set; }

	}

	public class PieChartData
	{
		[JsonPropertyName("name")]
		public string Name { get; set; }
        [JsonPropertyName("y")]
        public decimal Y { get; set; }
	}
}
