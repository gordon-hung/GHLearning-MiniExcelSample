using System.Net;
using System.Web;
using GHLearning.MiniExcelSample.Repositories;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using RichardSzalay.MockHttp;

namespace GHLearning.MiniExcelSample.RepositoriesTests;

public class RandomUserHttpClientTest
{
	[Fact]
	public async Task Normal_Process()
	{
		var fakeLog = NullLogger<RandomUserHttpClient>.Instance;
		var fakeTimeProvider = Substitute.For<TimeProvider>();
		var fakeHttpClientFactory = Substitute.For<IHttpClientFactory>();

		var baseAddress = new Uri("https://randomuser.me/");

		var results = 1;

		var response = """
			{
				"results": [
					{
						"gender": "male",
						"name": {
							"title": "Mr",
							"first": "Gerardo",
							"last": "Garza"
						},
						"location": {
							"street": {
								"number": 7172,
								"name": "Periférico Norte Rendón"
							},
							"city": "Huaniqueo de Morales",
							"state": "Queretaro",
							"country": "Mexico",
							"postcode": 20419,
							"coordinates": {
								"latitude": "25.8026",
								"longitude": "-171.3012"
							},
							"timezone": {
								"offset": "-2:00",
								"description": "Mid-Atlantic"
							}
						},
						"email": "gerardo.garza@example.com",
						"login": {
							"uuid": "556bc5a1-1277-4eaa-aab0-c05aaa00d167",
							"username": "sadbird789",
							"password": "november",
							"salt": "flCE3NP6",
							"md5": "c133b7749b57ec7322e6ab8fe8820c0e",
							"sha1": "798273ab4089c285a83faf6c06bfd5aca72c36da",
							"sha256": "d847c7729e8eaa132a6510c36fc06478f273e82962ae77a394b5f17c571ce82b"
						},
						"dob": {
							"date": "1946-04-12T10:25:23.691Z",
							"age": 78
						},
						"registered": {
							"date": "2009-01-15T22:08:51.017Z",
							"age": 16
						},
						"phone": "(666) 008 1446",
						"cell": "(675) 750 5793",
						"id": {
							"name": "NSS",
							"value": "37 19 07 1275 1"
						},
						"picture": {
							"large": "https://randomuser.me/api/portraits/men/64.jpg",
							"medium": "https://randomuser.me/api/portraits/med/men/64.jpg",
							"thumbnail": "https://randomuser.me/api/portraits/thumb/men/64.jpg"
						},
						"nat": "MX"
					}
				],
				"info": {
					"seed": "ef54eb251b85245d",
					"results": 1,
					"page": 1,
					"version": "1.4"
				}
			}
			""";

		var uriBuilder = new UriBuilder(new Uri(baseAddress, "/api"));
		var query = HttpUtility.ParseQueryString(uriBuilder.Query);
		query["results"] = results.ToString();
		uriBuilder.Query = query.ToString();

		var handler = new MockHttpMessageHandler();
		_ = handler.Expect(HttpMethod.Get, uriBuilder.Path + uriBuilder.Query)
		.Respond(HttpStatusCode.OK, new StringContent(response));

		var fakeHttpClient = handler.ToHttpClient();
		fakeHttpClient.BaseAddress = baseAddress;

		_ = fakeHttpClientFactory.CreateClient(name: Arg.Any<string>()).Returns(fakeHttpClient);

		var sut = new RandomUserHttpClient(fakeLog, fakeTimeProvider, fakeHttpClientFactory);

		var actual = await sut.RandomUserQueryAsync(results)
			.ToArrayAsync();

		Assert.Equal("sadbird789", actual.ElementAt(0).Username);
		Assert.Equal("Gerardo", actual.ElementAt(0).First);
		Assert.Equal("Garza", actual.ElementAt(0).Last);
		Assert.Equal("gerardo.garza@example.com", actual.ElementAt(0).Email);
		Assert.Equal(DateTimeOffset.Parse("1946-04-12T10:25:23.691Z"), actual.ElementAt(0).Birthday);
		Assert.Equal(DateTimeOffset.Parse("2009-01-15T22:08:51.017Z"), actual.ElementAt(0).Registered);
	}

	[Fact]
	public async Task Postcode_Converter_Integer()
	{
		var fakeLog = NullLogger<RandomUserHttpClient>.Instance;
		var fakeTimeProvider = Substitute.For<TimeProvider>();
		var fakeHttpClientFactory = Substitute.For<IHttpClientFactory>();

		var baseAddress = new Uri("https://randomuser.me/");

		var results = 1;

		var response = """
			{
				"results": [
					{
						"gender": "male",
						"name": {
							"title": "Mr",
							"first": "Gerardo",
							"last": "Garza"
						},
						"location": {
							"street": {
								"number": 7172,
								"name": "Periférico Norte Rendón"
							},
							"city": "Huaniqueo de Morales",
							"state": "Queretaro",
							"country": "Mexico",
							"postcode": 20419,
							"coordinates": {
								"latitude": "25.8026",
								"longitude": "-171.3012"
							},
							"timezone": {
								"offset": "-2:00",
								"description": "Mid-Atlantic"
							}
						},
						"email": "gerardo.garza@example.com",
						"login": {
							"uuid": "556bc5a1-1277-4eaa-aab0-c05aaa00d167",
							"username": "sadbird789",
							"password": "november",
							"salt": "flCE3NP6",
							"md5": "c133b7749b57ec7322e6ab8fe8820c0e",
							"sha1": "798273ab4089c285a83faf6c06bfd5aca72c36da",
							"sha256": "d847c7729e8eaa132a6510c36fc06478f273e82962ae77a394b5f17c571ce82b"
						},
						"dob": {
							"date": "1946-04-12T10:25:23.691Z",
							"age": 78
						},
						"registered": {
							"date": "2009-01-15T22:08:51.017Z",
							"age": 16
						},
						"phone": "(666) 008 1446",
						"cell": "(675) 750 5793",
						"id": {
							"name": "NSS",
							"value": "37 19 07 1275 1"
						},
						"picture": {
							"large": "https://randomuser.me/api/portraits/men/64.jpg",
							"medium": "https://randomuser.me/api/portraits/med/men/64.jpg",
							"thumbnail": "https://randomuser.me/api/portraits/thumb/men/64.jpg"
						},
						"nat": "MX"
					}
				],
				"info": {
					"seed": "ef54eb251b85245d",
					"results": 1,
					"page": 1,
					"version": "1.4"
				}
			}
			""";

		var uriBuilder = new UriBuilder(new Uri(baseAddress, "/api"));
		var query = HttpUtility.ParseQueryString(uriBuilder.Query);
		query["results"] = results.ToString();
		uriBuilder.Query = query.ToString();

		var handler = new MockHttpMessageHandler();
		_ = handler.Expect(HttpMethod.Get, uriBuilder.Path + uriBuilder.Query)
		.Respond(HttpStatusCode.OK, new StringContent(response));

		var fakeHttpClient = handler.ToHttpClient();
		fakeHttpClient.BaseAddress = baseAddress;

		_ = fakeHttpClientFactory.CreateClient(name: Arg.Any<string>()).Returns(fakeHttpClient);

		var client = new RandomUserHttpClient(fakeLog, fakeTimeProvider, fakeHttpClientFactory);

		var actual = await client.RandomUserQueryAsync(results)
			.ToArrayAsync();

		Assert.Equal("sadbird789", actual.ElementAt(0).Username);
		Assert.Equal("Gerardo", actual.ElementAt(0).First);
		Assert.Equal("Garza", actual.ElementAt(0).Last);
		Assert.Equal("gerardo.garza@example.com", actual.ElementAt(0).Email);
		Assert.Equal(DateTimeOffset.Parse("1946-04-12T10:25:23.691Z"), actual.ElementAt(0).Birthday);
		Assert.Equal(DateTimeOffset.Parse("2009-01-15T22:08:51.017Z"), actual.ElementAt(0).Registered);
	}

	[Fact]
	public async Task Postcode_Converter_IntegerString()
	{
		var fakeLog = NullLogger<RandomUserHttpClient>.Instance;
		var fakeTimeProvider = Substitute.For<TimeProvider>();
		var fakeHttpClientFactory = Substitute.For<IHttpClientFactory>();

		var baseAddress = new Uri("https://randomuser.me/");

		var results = 1;

		var response = """
			{
				"results": [
					{
						"gender": "male",
						"name": {
							"title": "Mr",
							"first": "Gerardo",
							"last": "Garza"
						},
						"location": {
							"street": {
								"number": 7172,
								"name": "Periférico Norte Rendón"
							},
							"city": "Huaniqueo de Morales",
							"state": "Queretaro",
							"country": "Mexico",
							"postcode": "20419",
							"coordinates": {
								"latitude": "25.8026",
								"longitude": "-171.3012"
							},
							"timezone": {
								"offset": "-2:00",
								"description": "Mid-Atlantic"
							}
						},
						"email": "gerardo.garza@example.com",
						"login": {
							"uuid": "556bc5a1-1277-4eaa-aab0-c05aaa00d167",
							"username": "sadbird789",
							"password": "november",
							"salt": "flCE3NP6",
							"md5": "c133b7749b57ec7322e6ab8fe8820c0e",
							"sha1": "798273ab4089c285a83faf6c06bfd5aca72c36da",
							"sha256": "d847c7729e8eaa132a6510c36fc06478f273e82962ae77a394b5f17c571ce82b"
						},
						"dob": {
							"date": "1946-04-12T10:25:23.691Z",
							"age": 78
						},
						"registered": {
							"date": "2009-01-15T22:08:51.017Z",
							"age": 16
						},
						"phone": "(666) 008 1446",
						"cell": "(675) 750 5793",
						"id": {
							"name": "NSS",
							"value": "37 19 07 1275 1"
						},
						"picture": {
							"large": "https://randomuser.me/api/portraits/men/64.jpg",
							"medium": "https://randomuser.me/api/portraits/med/men/64.jpg",
							"thumbnail": "https://randomuser.me/api/portraits/thumb/men/64.jpg"
						},
						"nat": "MX"
					}
				],
				"info": {
					"seed": "ef54eb251b85245d",
					"results": 1,
					"page": 1,
					"version": "1.4"
				}
			}
			""";

		var uriBuilder = new UriBuilder(new Uri(baseAddress, "/api"));
		var query = HttpUtility.ParseQueryString(uriBuilder.Query);
		query["results"] = results.ToString();
		uriBuilder.Query = query.ToString();

		var handler = new MockHttpMessageHandler();
		_ = handler.Expect(HttpMethod.Get, uriBuilder.Path + uriBuilder.Query)
		.Respond(HttpStatusCode.OK, new StringContent(response));

		var fakeHttpClient = handler.ToHttpClient();
		fakeHttpClient.BaseAddress = baseAddress;

		_ = fakeHttpClientFactory.CreateClient(name: Arg.Any<string>()).Returns(fakeHttpClient);

		var client = new RandomUserHttpClient(fakeLog, fakeTimeProvider, fakeHttpClientFactory);

		var actual = await client.RandomUserQueryAsync(results)
			.ToArrayAsync();

		Assert.Equal("sadbird789", actual.ElementAt(0).Username);
		Assert.Equal("Gerardo", actual.ElementAt(0).First);
		Assert.Equal("Garza", actual.ElementAt(0).Last);
		Assert.Equal("gerardo.garza@example.com", actual.ElementAt(0).Email);
		Assert.Equal(DateTimeOffset.Parse("1946-04-12T10:25:23.691Z"), actual.ElementAt(0).Birthday);
		Assert.Equal(DateTimeOffset.Parse("2009-01-15T22:08:51.017Z"), actual.ElementAt(0).Registered);
	}

	[Fact]
	public async Task Postcode_Converter_String()
	{
		var fakeLog = NullLogger<RandomUserHttpClient>.Instance;
		var fakeTimeProvider = Substitute.For<TimeProvider>();
		var fakeHttpClientFactory = Substitute.For<IHttpClientFactory>();

		var baseAddress = new Uri("https://randomuser.me/");

		var results = 1;

		var response = """
			{
				"results": [
					{
						"gender": "male",
						"name": {
							"title": "Mr",
							"first": "Gerardo",
							"last": "Garza"
						},
						"location": {
							"street": {
								"number": 7172,
								"name": "Periférico Norte Rendón"
							},
							"city": "Huaniqueo de Morales",
							"state": "Queretaro",
							"country": "Mexico",
							"postcode": "I7 3SN",
							"coordinates": {
								"latitude": "25.8026",
								"longitude": "-171.3012"
							},
							"timezone": {
								"offset": "-2:00",
								"description": "Mid-Atlantic"
							}
						},
						"email": "gerardo.garza@example.com",
						"login": {
							"uuid": "556bc5a1-1277-4eaa-aab0-c05aaa00d167",
							"username": "sadbird789",
							"password": "november",
							"salt": "flCE3NP6",
							"md5": "c133b7749b57ec7322e6ab8fe8820c0e",
							"sha1": "798273ab4089c285a83faf6c06bfd5aca72c36da",
							"sha256": "d847c7729e8eaa132a6510c36fc06478f273e82962ae77a394b5f17c571ce82b"
						},
						"dob": {
							"date": "1946-04-12T10:25:23.691Z",
							"age": 78
						},
						"registered": {
							"date": "2009-01-15T22:08:51.017Z",
							"age": 16
						},
						"phone": "(666) 008 1446",
						"cell": "(675) 750 5793",
						"id": {
							"name": "NSS",
							"value": "37 19 07 1275 1"
						},
						"picture": {
							"large": "https://randomuser.me/api/portraits/men/64.jpg",
							"medium": "https://randomuser.me/api/portraits/med/men/64.jpg",
							"thumbnail": "https://randomuser.me/api/portraits/thumb/men/64.jpg"
						},
						"nat": "MX"
					}
				],
				"info": {
					"seed": "ef54eb251b85245d",
					"results": 1,
					"page": 1,
					"version": "1.4"
				}
			}
			""";

		var uriBuilder = new UriBuilder(new Uri(baseAddress, "/api"));
		var query = HttpUtility.ParseQueryString(uriBuilder.Query);
		query["results"] = results.ToString();
		uriBuilder.Query = query.ToString();

		var handler = new MockHttpMessageHandler();
		_ = handler.Expect(HttpMethod.Get, uriBuilder.Path + uriBuilder.Query)
		.Respond(HttpStatusCode.OK, new StringContent(response));

		var fakeHttpClient = handler.ToHttpClient();
		fakeHttpClient.BaseAddress = baseAddress;

		_ = fakeHttpClientFactory.CreateClient(name: Arg.Any<string>()).Returns(fakeHttpClient);

		var client = new RandomUserHttpClient(fakeLog, fakeTimeProvider, fakeHttpClientFactory);

		var actual = await client.RandomUserQueryAsync(results)
			.ToArrayAsync();

		Assert.Equal("sadbird789", actual.ElementAt(0).Username);
		Assert.Equal("Gerardo", actual.ElementAt(0).First);
		Assert.Equal("Garza", actual.ElementAt(0).Last);
		Assert.Equal("gerardo.garza@example.com", actual.ElementAt(0).Email);
		Assert.Equal(DateTimeOffset.Parse("1946-04-12T10:25:23.691Z"), actual.ElementAt(0).Birthday);
		Assert.Equal(DateTimeOffset.Parse("2009-01-15T22:08:51.017Z"), actual.ElementAt(0).Registered);
	}
}