using System.Text.Json.Serialization;

namespace GHLearning.MiniExcelSample.Repositories.Models;

public record RandomUseResulLocationTimezone
{
	[JsonPropertyName("offset")]
	public required string Offset { get; set; }
	[JsonPropertyName("description")]
	public required string Description { get; set; }
}
