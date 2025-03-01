using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Web;
using GHLearning.MiniExcelSample.Core;
using GHLearning.MiniExcelSample.Core.Models;
using GHLearning.MiniExcelSample.Repositories.Models;
using Microsoft.Extensions.Logging;

namespace GHLearning.MiniExcelSample.Repositories;

internal class RandomUserHttpClient(
	ILogger<RandomUserHttpClient> logger,
	TimeProvider timeProvider,
	IHttpClientFactory httpClientFactory) : IRandomUserHttpClient
{
	public async IAsyncEnumerable<UserInfo> RandomUserQueryAsync(int results = 1, [EnumeratorCancellation] CancellationToken cancellationToken = default)
	{
		using var client = httpClientFactory.CreateClient("RandomUserApi");
		var uriBuilder = new UriBuilder(new Uri(client.BaseAddress!, "/api"));
		var query = HttpUtility.ParseQueryString(uriBuilder.Query);
		query["results"] = results.ToString();
		uriBuilder.Query = query.ToString();

		var sw = Stopwatch.StartNew();

		var httpResponseMessage = await client.GetAsync(
			uriBuilder.ToString(),
			cancellationToken)
		.ConfigureAwait(false);

		sw.Stop();

		var responseMessage = default(string);
		using (var sr = new StreamReader(httpResponseMessage.Content.ReadAsStreamAsync(cancellationToken).Result))
		{
			responseMessage = await sr.ReadToEndAsync(cancellationToken: cancellationToken).ConfigureAwait(false) ?? string.Empty;
		}

		var logEvent = new
		{
			Path = httpResponseMessage.RequestMessage?.RequestUri?.AbsoluteUri ?? string.Empty,
			Method = httpResponseMessage.RequestMessage?.Method.ToString() ?? string.Empty,
			ServerName = Environment.MachineName,
			RequestHeaders = httpResponseMessage.RequestMessage?.Headers + httpResponseMessage.RequestMessage?.Content?.Headers?.ToString(),
			ResponseHeaders = httpResponseMessage.Headers.ToString(),
			ResponseContent = responseMessage,
			ResponseCode = (short)httpResponseMessage.StatusCode,
			ExecutionTimeMs = (int)sw.ElapsedMilliseconds,
			RecordedOn = timeProvider.GetUtcNow()
		};

		logger.LogInformation("{logEvent}", JsonSerializer.Serialize(logEvent));

		httpResponseMessage.EnsureSuccessStatusCode();

		var randomUserResponse = JsonSerializer.Deserialize<RandomUserResponse>(responseMessage);

		ArgumentNullException.ThrowIfNull(randomUserResponse, nameof(randomUserResponse));

		foreach (var result in randomUserResponse.Results)
		{
			yield return new UserInfo(
				result.Login.Username,
				result.Name.First,
				result.Name.Last,
				result.Email,
				result.Dob.Date,
				result.Registered.Date);
		}
	}
}
