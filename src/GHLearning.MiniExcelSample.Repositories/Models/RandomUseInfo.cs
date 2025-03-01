using System.Text.Json.Serialization;

namespace GHLearning.MiniExcelSample.Repositories.Models;

public record RandomUseInfo
{
	[JsonPropertyName("seed")]
	public required string Seed { get; set; }
	[JsonPropertyName("results")]
	public int Results { get; set; }
	[JsonPropertyName("page")]
	public int Page { get; set; }
	[JsonPropertyName("version")]
	public required string Version { get; set; }
}
