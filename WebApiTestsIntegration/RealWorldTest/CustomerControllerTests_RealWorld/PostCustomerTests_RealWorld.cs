using Bogus;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Net.Http.Json;
using WebApi.Entities;

namespace WebApiTestsIntegration;

public class PostCustomerTests_RealWorld : IClassFixture<CustomerApiFactory>
{
    private readonly CustomerApiFactory _appFactory;
    private readonly HttpClient _httpClient;
    private readonly Faker<Customer> _fakeCustomerGenetator = new Faker<Customer>()
            .RuleFor(m => m.Name, faker => faker.Person.FullName)
            .RuleFor(m => m.Id, faker => faker.Database.Random.Int(4))
            .RuleFor(m => m.Credit, faker => faker.Finance.Random.Decimal())
            .RuleFor(m => m.CreatedDate, DateTime.Now)
            ;
    
    public PostCustomerTests_RealWorld(CustomerApiFactory appFactory)
    {
        _appFactory = appFactory;
        _httpClient = appFactory.CreateClient();
    }

    [Fact]
    public async Task Post_CreatesUser_WhenDataIsValid() 
    {
        //Arrange
        var testCustomer = _fakeCustomerGenetator.Generate();

        //Act
        var response = await _httpClient.PostAsJsonAsync("/api/customer", testCustomer);

        //Assert
        var custResponse = await response.Content.ReadFromJsonAsync<Customer>();
        testCustomer.Id = custResponse!.Id;
        custResponse.Should().BeEquivalentTo(testCustomer);
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
    }

    //[Fact]
    //public async Task DbInitializationTest()
    //{
    //    await Task.Delay(10000);
    //}
}
