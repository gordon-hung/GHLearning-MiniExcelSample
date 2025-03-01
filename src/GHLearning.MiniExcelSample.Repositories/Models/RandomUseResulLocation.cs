using System.Text.Json.Serialization;
using GHLearning.MiniExcelSample.Repositories.JsonConverters;

namespace GHLearning.MiniExcelSample.Repositories.Models;

public record RandomUseResulLocation
{
	[JsonPropertyName("street")]
	public required RandomUseResulLocationStreet Street { get; set; }
	[JsonPropertyName("city")]
	public required string City { get; set; }
	[JsonPropertyName("state")]
	public required string State { get; set; }
	[JsonPropertyName("country")]
	public required string Country { get; set; }
	[JsonPropertyName("postcode")]
	[JsonConverter(typeof(PostcodeConverter))]
	public required string Postcode { get; set; }
	[JsonPropertyName("coordinates")]
	public required RandomUseResulLocationCoordinates Coordinates { get; set; }
	[JsonPropertyName("timezone")]
	public required RandomUseResulLocationTimezone Timezone { get; set; }
}
