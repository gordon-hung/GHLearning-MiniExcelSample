using GHLearning.MiniExcelSample.Core.Models;

namespace GHLearning.MiniExcelSample.Core;

public interface IRandomUserHttpClient
{
	IAsyncEnumerable<UserInfo> RandomUserQueryAsync(int results = 1, CancellationToken cancellationToken = default);
}
