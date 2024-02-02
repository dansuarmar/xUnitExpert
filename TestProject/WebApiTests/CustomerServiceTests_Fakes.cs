using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Services;
using WebApi.Repositories;
using FluentAssertions;
using TestProject.WebApiTests.Fakes;

namespace TestProject.WebApiTests
{
    public class CustomerServiceTests_Fakes
    {
        private readonly CustomerService _sut;
        public CustomerServiceTests_Fakes()
        {
            _sut = new CustomerService(new FakeCustomerRepository());
        }

        [Fact]
        public async Task GetTaskAsync_ShouldReturnEmpty_WhenNoCustomerExist() 
        {
            //Arrange
            //Act
            var customers = await _sut.GetAllAsync();

            //Assert
            customers.Should().BeEmpty();
        }        
        
        [Fact]
        public async Task GetTaskAsync_ShouldReturnCustomers_WhenExists() 
        {
            //Arrange
            await _sut.AddAsync(new WebApi.Entities.Customer 
            {
                Name = "name",
                Id = 1,
                CreatedDate = DateTime.Now,
                Credit = 1000m,
            });

            //Act
            var customers = await _sut.GetAllAsync();

            //Assert
            customers.Should().NotBeEmpty();
        }
    }
}
