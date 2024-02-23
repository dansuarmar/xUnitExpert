using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApi;

namespace WebApiTestsIntegration
{
    public class CustomerControllerTests
    {

        private readonly WebApplicationFactory<IApiMarker> _appFactory = new();
        private readonly HttpClient _httpClient;

        public CustomerControllerTests()
        {
            _httpClient = _appFactory.CreateClient();
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenCurtomerDoesNotExists() 
        {
            //Arrange
            //Act
            var response = await _httpClient.GetAsync($"/api/customer/4");

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
