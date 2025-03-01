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
		var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "RandomUserTemplates.xlsx");
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

		using var stream = new MemoryStream();
		stream.SaveAsByTemplate(path, data);
		return stream.ToArray();
	}
}
