namespace GHLearning.MiniExcelSample.Core.Models;

public record UserInfo(
	string Username,
	string First,
	string Last,
	string Email,
	DateTimeOffset Birthday,
	DateTimeOffset Registered);
