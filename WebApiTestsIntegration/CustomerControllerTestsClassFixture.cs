using Bogus;
using Bogus.Extensions;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.ProjectModel;
using NuGet.Packaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WebApi;
using WebApi.Entities;

namespace WebApiTestsIntegration
{
    public class CustomerControllerTests_ClassFixture : IClassFixture<WebApplicationFactory<IApiMarker>>, IAsyncLifetime
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

        public CustomerControllerTests_ClassFixture(WebApplicationFactory<IApiMarker> appFactory)
        {
            _appFactory = appFactory;
            _httpClient = _appFactory.CreateClient();
            _createdIds = new();
        }

        [Fact]
        public async Task Post_ReturnsOk_WhenNewCustomerCreated()
        {
            //Arrange
            var customer = _fakeCustomerGenetator.Generate();

            //Act
            var response = await _httpClient.PostAsJsonAsync($"/api/customer/", customer);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var customerResponse = await response.Content.ReadFromJsonAsync<Customer>();
            customer.Id = customerResponse!.Id;
            customerResponse.Should().BeEquivalentTo(customer);
            _createdIds.Add(customer.Id);
        }

        [Fact]
        public async Task Get_ReturnsCustomer_WhenCustomerExists()
        {
            //Arrange
            //Act
            var response = await _httpClient.GetAsync($"/api/customer/3");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            //Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenCurtomerDoesNotExists() 
        {
            //Arrange
            //Act
            var response = await _httpClient.GetAsync($"/api/customer/4");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            //Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            
            //Check the Content.
            //This is to read it as text. But for a test is too simple.
            var responseText = await response.Content.ReadAsStringAsync();
            responseText.Should().Contain("404");
            //You can also read it as an object.
            var problem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            problem!.Title.Should().Be("Not Found");
            problem.Status.Should().Be(404);

            //Check the Headers
            //For standard headers.
            response!.Headers!.Location!.Should().BeNull();
            //For non standard headers.
            
            //response.Headers.GetValues("Location").Should().BeNull();
        }

        public Task InitializeAsync() => Task.CompletedTask;
        public async Task DisposeAsync()
        {
            foreach(var customerId in _createdIds)
                await _httpClient.DeleteAsync($"/api/customer/{customerId}");
        }
    }
}
