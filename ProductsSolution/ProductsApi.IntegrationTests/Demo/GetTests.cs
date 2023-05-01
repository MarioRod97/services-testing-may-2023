using Alba;
using Microsoft.Extensions.DependencyInjection;
using ProductsApi.Demo;

namespace ProductsApi.IntegrationTests.Demo;

public class GetTests
{
    [Fact]
    public async Task Get200StatusCode()
    {
        await using var host = await AlbaHost.For<Program>(options =>
        {
            options.ConfigureServices((context, sp) =>
            {
                sp.AddSingleton<ISystemClock, FakeTestingClock>();
            });
        });

        var expectedResponse = new DemoResponse
        {
            Message = "Hello from the other side!",
            CreatedAt = new DateTimeOffset(new DateTime(1969, 4, 20, 23, 59, 00), TimeSpan.FromHours(-4))
        };

        // Scenarios
        var response = await host.Scenario(api =>
        {
            api.Get.Url("/demo");
            api.StatusCodeShouldBeOk();
            api.Header("content-type").ShouldHaveValues("application/json; charset=utf-8");
        });

        var actualResponse = response.ReadAsJson<DemoResponse>();

        Assert.Equal(expectedResponse, actualResponse);
    }
}

public class FakeTestingClock : ISystemClock
{
    public DateTimeOffset GetCurrent()
    {
        return new DateTimeOffset(new DateTime(1969, 4, 20, 23, 59, 00), TimeSpan.FromHours(-4));
    }
}
