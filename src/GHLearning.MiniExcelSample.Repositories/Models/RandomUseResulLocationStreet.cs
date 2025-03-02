using System.Text.Json.Serialization;

namespace GHLearning.MiniExcelSample.Repositories.Models;

public record RandomUseResulLocationStreet
{
	[JsonPropertyName("number")]
	public int Number { get; set; }
	[JsonPropertyName("name")]
	public required string Name { get; set; }
}
