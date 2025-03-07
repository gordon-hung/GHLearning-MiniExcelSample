﻿using System.Net;
using System.Net.Sockets;
using System.Reflection;
using GHLearning.MiniExcelSample.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GHLearning.MiniExcelSample.ApiService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InfoController : ControllerBase
{
	[HttpGet]
	public async Task<object> GetAsync(
		[FromServices] IWebHostEnvironment hostingEnvironment,
		[FromServices] IConfiguration configuration,
		[FromServices] IOptions<RandomUserOptions> randomUserOptions)
	{
		var hostName = Dns.GetHostName();
		var hostEntry = await Dns.GetHostEntryAsync(hostName).ConfigureAwait(false);
		var hostIp = Array.Find(hostEntry.AddressList,
			x => x.AddressFamily == AddressFamily.InterNetwork);

		return new
		{
			Environment.MachineName,
			HostName = hostName,
			HostIp = hostIp?.ToString() ?? string.Empty,
			Environment = hostingEnvironment.EnvironmentName,
			OsVersion = $"{Environment.OSVersion}",
			Version = Assembly.GetExecutingAssembly().GetName().Version?.ToString(),
			ProcessCount = Environment.ProcessorCount,
			ConnectionStrings = configuration.GetSection("ConnectionStrings").GetChildren(),
			RandomUserOptions = randomUserOptions.Value
		};
	}
}
