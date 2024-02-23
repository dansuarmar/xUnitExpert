using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WebApi;

namespace WebApiTestsIntegration
{
    public class CustomerControllerTests_ClassFixture : IClassFixture<WebApplicationFactory<IApiMarker>>
    {

        private readonly WebApplicationFactory<IApiMarker> _appFactory;
        private readonly HttpClient _httpClient;

        public CustomerControllerTests_ClassFixture(WebApplicationFactory<IApiMarker> appFactory)
        {
            _appFactory = appFactory;
            _httpClient = _appFactory.CreateClient();
        }

        [Fact]
        public async Task Get_ReturnsCustomer_WhenExists()
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
    }
}
