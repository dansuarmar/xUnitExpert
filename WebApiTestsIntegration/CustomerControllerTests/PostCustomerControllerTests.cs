
using Bogus;
using Microsoft.AspNetCore.Mvc.Testing;
using WebApi;
using WebApi.Entities;

namespace WebApiTestsIntegration;

public class PostCustomerControllerTests : IClassFixture<WebApplicationFactory<IApiMarker>>, IAsyncLifetime
{
    private readonly WebApplicationFactory<IApiMarker> _appFactory;
    private readonly HttpClient _httpClient;
    private readonly Faker<Customer> _fakeCustomerGenetator = new Faker<Customer>()
        .RuleFor(m => m.Name, faker => faker.Person.FullName)
        .RuleFor(m => m.Id, faker => faker.Database.Random.Int(4))
        .RuleFor(m => m.Credit, faker => faker.Finance.Random.Decimal())
        .RuleFor(m => m.CreatedDate, DateTime.Now)
        ;
    private readonly List<int> _createdIds;

    public PostCustomerControllerTests(WebApplicationFactory<IApiMarker> appFactory)
    {
        _appFactory = appFactory;
        _httpClient = _appFactory.CreateClient();
        _createdIds = [];
    }

    public Task InitializeAsync() => Task.CompletedTask;
    public async Task DisposeAsync()
    {
        foreach (var customerId in _createdIds)
            await _httpClient.DeleteAsync($"/api/customer/{customerId}");
    }
}
