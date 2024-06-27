using System.Text.Json.Serialization;

namespace ToyProj.Models
{
	public class ColumChartViewModel
	{
		[JsonPropertyName("name")]
		public string Name { get; set; }
		[JsonPropertyName("data")]
		public long[] Data { get; set; }
	}
}
