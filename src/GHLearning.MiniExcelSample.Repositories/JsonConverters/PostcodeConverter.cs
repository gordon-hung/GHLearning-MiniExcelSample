using System.Text.Json;

using System.Text.Json.Serialization;

namespace GHLearning.MiniExcelSample.Repositories.JsonConverters;

public class PostcodeConverter : JsonConverter<string>
{
	public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType == JsonTokenType.Number)
		{
			return reader.GetInt32().ToString();
		}
		else if (reader.TokenType == JsonTokenType.String)
		{
			return reader.GetString() ?? string.Empty;
		}

		throw new JsonException("Unexpected token type.");
	}

	public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
	{
		writer.WriteStringValue(value);
	}
}
