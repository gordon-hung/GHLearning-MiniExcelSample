using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GHLearning.MiniExcelSample.Core;
using System.Web;
using NSubstitute;
using GHLearning.MiniExcelSample.Core.Models;
using GHLearning.MiniExcelSample.Core.ApplicationServices;
using static System.Runtime.InteropServices.JavaScript.JSType;
using MiniExcelLibs;

namespace GHLearning.MiniExcelSample.CoreTests.ApplicationServices;

public class RandomUserRequestHandlerTest
{
	[Fact]
	public async Task Normal_Process()
	{
		var fakeRandomUserHttpClient = Substitute.For<IRandomUserHttpClient>();

		var request = new RandomUserRequest(Results: 1);

		var userInfo = new UserInfo(
			Username: "sadbird789",
			First: "Gerardo",
			Last: "Garza",
			Email: "gerardo.garza@example.com",
			Birthday: DateTimeOffset.Parse("1946-04-12T10:25:23.691Z"),
			Registered: DateTimeOffset.Parse("2009-01-15T22:08:51.017Z"));

		_ = fakeRandomUserHttpClient.RandomUserQueryAsync(
			Arg.Any<int>(),
			Arg.Any<CancellationToken>())
			.Returns(new[] { userInfo }.ToAsyncEnumerable());

		var sut = new RandomUserRequestHandler(fakeRandomUserHttpClient);

		var cancellationTokenSource = new CancellationTokenSource();

		_ = await sut.Handle(request, cancellationTokenSource.Token);
	}
}
