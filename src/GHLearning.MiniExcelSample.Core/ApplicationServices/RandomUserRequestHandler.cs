using MediatR;
using MiniExcelLibs;

namespace GHLearning.MiniExcelSample.Core.ApplicationServices;

internal class RandomUserRequestHandler(
	IRandomUserHttpClient client) : IRequestHandler<RandomUserRequest, byte[]>
{
	public async Task<byte[]> Handle(RandomUserRequest request, CancellationToken cancellationToken)
	{
		var userInfos = await client.RandomUserQueryAsync(
			results: request.Results,
			cancellationToken: cancellationToken)
			.ToArrayAsync(
			cancellationToken: cancellationToken)
			.ConfigureAwait(false);

		var data = new
		{
			UserInfos = userInfos.Select(userInfo => new
			{
				userInfo.Username,
				userInfo.First,
				userInfo.Last,
				userInfo.Email,
				Birthday = userInfo.Birthday.ToString("yyyy-MM-dd"),
				Registered = userInfo.Registered.UtcDateTime.ToString("u")
			})
		};

		var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "RandomUserTemplates.xlsx");

		using var stream = new MemoryStream();
		stream.SaveAsByTemplate(templatePath: path, value: data);
		return stream.ToArray();
	}
}
