using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Services;
using WebApi.Repositories;
using FluentAssertions;
using TestProject.WebApiTests.Fakes;
using NSubstitute;
using WebApi.Entities;

namespace TestProject.WebApiTests
{
    public class CustomerServiceTests_Mocks
    {
        private readonly CustomerService _sut;
        private readonly ICustomerRepository _repository;

        public CustomerServiceTests_Mocks()
        {
            //This creates the repository.
            _repository = Substitute.For<ICustomerRepository>();
            _sut = new CustomerService(_repository);
        }

        [Fact]
        public async Task GetTaskAsync_ShouldReturnEmpty_WhenNoCustomerExist() 
        {
            //Arrange
            _repository.GetAllAsync().Returns(Array.Empty<Customer>());

            //Act
            var customers = await _sut.GetAllAsync();

            //Assert
            customers.Should().BeEmpty();
        }        
        
        [Fact]
        public async Task GetTaskAsync_ShouldReturnCustomers_WhenExists() 
        {
            //Arrange
            Customer[] array = [
            (
                new Customer 
                {
                    Name = "name",
                    Id = 1,
                    CreatedDate = DateTime.Now,
                    Credit = 1000m,
                }
            )];
            _repository.GetAllAsync().Returns(array);

            //Act
            var customers = await _sut.GetAllAsync();

            //Assert
            customers.Should().NotBeEmpty();
            customers.Should().HaveCount(1);
            customers.Should().ContainSingle(x => x.Name == "name");
        }
    }
}
