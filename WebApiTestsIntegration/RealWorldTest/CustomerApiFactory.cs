using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Testcontainers.PostgreSql;
using WebApi;
using WebApi.Database;

namespace WebApiTestsIntegration;

public class CustomerApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer =
        new PostgreSqlBuilder()
        .WithDatabase("mydb")
        .WithUsername("workshop")
        .WithPassword("changeme")
        //.WithPortBinding(5432) //It's random, its provided with the ConnectionString generated.
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(logging => 
        {
            logging.ClearProviders();
        });

        builder.ConfigureTestServices(services => 
        {
            services.RemoveAll(typeof(IDbConnectionFactory));
            services.AddSingleton<IDbConnectionFactory>(_ => new NpgsqlConnectionFactory(_dbContainer.GetConnectionString()));
        });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
    }
}

// private readonly TestcontainersContainer _dbContainer =
//     new TestcontainersBuilder<TestcontainersContainer>()
//         .WithImage("postgres:latest")
//         .WithEnvironment("POSTGRES_USER", "course")
//         .WithEnvironment("POSTGRES_PASSWORD", "changeme")
//         .WithEnvironment("POSTGRES_DB", "mydb")
//         .WithPortBinding(5555, 5432)
//         .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5432))
//         .Build();