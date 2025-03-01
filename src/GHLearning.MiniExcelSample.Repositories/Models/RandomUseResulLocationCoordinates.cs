using System.Text.Json.Serialization;

namespace GHLearning.MiniExcelSample.Repositories.Models;

public record RandomUseResulLocationCoordinates
{
	[JsonPropertyName("latitude")]
	public required string Latitude { get; set; }
	[JsonPropertyName("longitude")]
	public required string Longitude { get; set; }
}