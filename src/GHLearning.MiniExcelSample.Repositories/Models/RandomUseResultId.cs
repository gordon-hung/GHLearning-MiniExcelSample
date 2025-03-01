using System.Text.Json.Serialization;

namespace GHLearning.MiniExcelSample.Repositories.Models;

public record RandomUseResultId
{
	[JsonPropertyName("name")]
	public required string Name { get; set; }
	[JsonPropertyName("value")]
	public required string Value { get; set; }
}
