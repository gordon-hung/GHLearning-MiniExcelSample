using System.Text.Json.Serialization;

namespace GHLearning.MiniExcelSample.Repositories.Models;

public record RandomUserResponse
{
	[JsonPropertyName("results")]
	public required IReadOnlyCollection<RandomUseResult> Results { get; set; }
	[JsonPropertyName("info")]
	public required RandomUseInfo Info { get; set; }
}