using MediatR;

namespace GHLearning.MiniExcelSample.Core.ApplicationServices;

public record RandomUserRequest(
	int Results) : IRequest<byte[]>;
