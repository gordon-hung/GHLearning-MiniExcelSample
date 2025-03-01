using System.Text.Json.Serialization;

namespace GHLearning.MiniExcelSample.Repositories.Models;

public record RandomUseResultPicture
{
	[JsonPropertyName("large")]
	public required string Large { get; set; }
	[JsonPropertyName("medium")]
	public required string Medium { get; set; }
	[JsonPropertyName("thumbnail")]
	public required string Thumbnail { get; set; }
}
