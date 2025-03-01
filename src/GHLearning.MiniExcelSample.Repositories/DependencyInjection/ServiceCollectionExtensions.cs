using GHLearning.MiniExcelSample.Core;
using GHLearning.MiniExcelSample.Repositories;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddGHLearningMiniExcelSampleRepositories(
		this IServiceCollection services)
		=> services
		.AddHttpClient(
			name: "RandomUserApi",
			configureClient: (serviceProvider, client) => client.BaseAddress = serviceProvider.GetRequiredService<IOptions<RandomUserOptions>>().Value.BaseUri)
		.Services
		.AddTransient<IRandomUserHttpClient, RandomUserHttpClient>();
}
