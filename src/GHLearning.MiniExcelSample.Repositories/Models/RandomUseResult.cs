using System.Text.Json.Serialization;

namespace GHLearning.MiniExcelSample.Repositories.Models;

public record RandomUseResult
{
	[JsonPropertyName("gender")]
	public required string Gender { get; set; }
	[JsonPropertyName("name")]
	public required RandomUseResultName Name { get; set; }
	[JsonPropertyName("location")]
	public required RandomUseResulLocation Location { get; set; }
	[JsonPropertyName("email")]
	public required string Email { get; set; }
	[JsonPropertyName("login")]
	public required RandomUseResulLogin Login { get; set; }
	[JsonPropertyName("dob")]
	public required RandomUseResultDob Dob { get; set; }
	[JsonPropertyName("registered")]
	public required RandomUseResultRegistered Registered { get; set; }
	[JsonPropertyName("phone")]
	public required string Phone { get; set; }
	[JsonPropertyName("cell")]
	public required string Cell { get; set; }
	[JsonPropertyName("id")]
	public required RandomUseResultId Id { get; set; }
	[JsonPropertyName("picture")]
	public required RandomUseResultPicture Picture { get; set; }
	[JsonPropertyName("nat")]
	public required string Nat { get; set; }
}
