using System.Text.Json.Serialization;

namespace GHLearning.MiniExcelSample.Repositories.Models;

public record RandomUseResulLogin
{
	[JsonPropertyName("uuid")]
	public required string Uuid { get; set; }
	[JsonPropertyName("username")]
	public required string Username { get; set; }
	[JsonPropertyName("password")]
	public required string Password { get; set; }
	[JsonPropertyName("salt")]
	public required string Salt { get; set; }
	[JsonPropertyName("md5")]
	public required string Md5 { get; set; }
	[JsonPropertyName("sha1")]
	public required string Sha1 { get; set; }
	[JsonPropertyName("sha256")]
	public required string Sha256 { get; set; }
}
