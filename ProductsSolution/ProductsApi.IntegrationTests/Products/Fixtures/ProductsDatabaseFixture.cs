using Alba;
using Marten;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.PostgreSql;
using WireMock.Server;

namespace ProductsApi.IntegrationTests.Products.Fixtures;

public class ProductsDatabaseFixture : IAsyncLifetime
{
    private readonly string PG_IMAGE = "jeffrygonzalez/sdt-may-products-empty:latest";

    public IAlbaHost AlbaHost = null!;
    private readonly PostgreSqlContainer _pgContainer;
    public WireMockServer MockServer = null!;

    public ProductsDatabaseFixture()
    {
        _pgContainer = new PostgreSqlBuilder()
            .WithDatabase("products_dev")
            .WithUsername("user")
            .WithPassword("password")
            // .WithPortBinding(5432, 5432)
            .WithImage(PG_IMAGE)
            .Build();
    }

    public async Task InitializeAsync()
    {
        MockServer = WireMockServer.Start(1338);
        
        await _pgContainer.StartAsync();

        AlbaHost = await Alba.AlbaHost.For<Program>(builder =>
        {
            builder.ConfigureServices(services =>
            {
                ConfigureAdditionalServices(services);
                services.AddMarten(options =>
                {
                    var connectionString = _pgContainer.GetConnectionString();
                    options.Connection(connectionString);
                });
            });
        });
    }

    protected virtual void ConfigureAdditionalServices(IServiceCollection services)
    {
        // to do
    }

    public async Task DisposeAsync()
    {
        MockServer.Stop();
        await AlbaHost.DisposeAsync();
        await _pgContainer.DisposeAsync();
    }
}
