using GHLearning.MiniExcelSample.Core;
using GHLearning.MiniExcelSample.Core.ApplicationServices;
using GHLearning.MiniExcelSample.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GHLearning.MiniExcelSample.ApiService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RandomUserController : ControllerBase
{
	/// <summary>
	/// Randoms the user query asynchronous.
	/// </summary>
	/// <param name="client">The client.</param>
	/// <param name="results">The results.</param>
	/// <returns></returns>
	[HttpGet]
	public IAsyncEnumerable<UserInfo> RandomUserQueryAsync(
		[FromServices] IRandomUserHttpClient client,
		[FromQuery] int results = 1)
		=> client.RandomUserQueryAsync(
			results,
			HttpContext.RequestAborted);

	[HttpGet("Export")]
	public async Task<IActionResult> ExportByReportTemplateAsync(
		[FromServices] TimeProvider timeProvider,
		[FromServices] IMediator mediator,
		[FromQuery] int results = 1)
	{
		return File(await mediator.Send(new RandomUserRequest(results)).ConfigureAwait(false),
			"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
			$"RandomUser_{timeProvider.GetUtcNow().ToUnixTimeSeconds()}.xlsx");
	}
}
