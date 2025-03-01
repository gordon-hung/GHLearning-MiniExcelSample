using System.Text.Json.Serialization;

namespace GHLearning.MiniExcelSample.Repositories.Models;

public record RandomUseResultName
{
	[JsonPropertyName("title")]
	public required string Title { get; set; }
	[JsonPropertyName("first")]
	public required string First { get; set; }
	[JsonPropertyName("last")]
	public required string Last { get; set; }
}
