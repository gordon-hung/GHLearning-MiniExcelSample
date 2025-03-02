using System.Text.Json.Serialization;

namespace GHLearning.MiniExcelSample.Repositories.Models;

public record RandomUseResultRegistered
{
	[JsonPropertyName("date")]
	public DateTimeOffset Date { get; set; }
	[JsonPropertyName("age")]
	public int Age { get; set; }
}
