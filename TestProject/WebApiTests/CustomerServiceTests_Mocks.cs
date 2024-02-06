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
using Castle.Core.Logging;
using WebApi;
using NSubstitute.Core.Arguments;
using NSubstitute.ExceptionExtensions;

namespace TestProject.WebApiTests
{
    public class CustomerServiceTests_Mocks
    {
        private readonly CustomerService _sut;
        private readonly ICustomerRepository _repository;
        private readonly ILoggerAdapter<CustomerService> _logger;

        public CustomerServiceTests_Mocks()
        {
            //This creates the repository.
            _repository = Substitute.For<ICustomerRepository>();
            _logger = Substitute.For<ILoggerAdapter<CustomerService>>();
            _sut = new CustomerService(_repository, _logger);
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
            var user = new Customer
            {
                Name = "name",
                Id = 1,
                CreatedDate = DateTime.Now,
                Credit = 1000m,
            };

            //Arrange
            var array = new[]{
                user,
            };
            _repository.GetAllAsync().Returns(array);

            //Act
            var customers = await _sut.GetAllAsync();

            //Assert
            customers.Should().NotBeEmpty();
            customers.Should().HaveCount(1);
            customers.Should().ContainSingle().Equals(user);
            customers.Should().ContainSingle(x => x.Name == "name");
        }

        [Fact]
        public async Task GetAllAsync_ShouldLogMessages_WhenInoked()
        {
            //Arrange
            _repository.GetAllAsync().Returns(Array.Empty<Customer>());

            //Act
            var customers = await _sut.GetAllAsync();

            //Assert
            _logger.Received(1).LogInformation("Retriving all Customers."); //This 2 calls do exactly the same validation.
            _logger.Received(1).LogInformation(Arg.Is("Retriving all Customers."));
            _logger.Received(1).LogInformation(Arg.Is<string?>(x => x!.StartsWith("Retriving")));
            _logger.Received(1).LogInformation(Arg.Is("Customers retrived in {0}ms"), Arg.Any<long>());
        }

        [Fact]
        public async Task GetAll_ShouldLogException_WhenExceptionIsThrown()
        {
            //Arrange
            var exception = new Exception("Test Exception");
            _repository.GetAllAsync().Throws(exception);

            //Act
            var result = async () => await _sut.GetAllAsync(); 

            //Assert
            // await result.Should().ThrowAsync<Exception>(); //If you only want to check for the Exception to be thrown.
            await result.Should().ThrowAsync<Exception>().WithMessage("Test Exception");
            _logger.Received(1).LogError(Arg.Is(exception), Arg.Is("Something went wrong when retriving Customers."));
        }
    }
}
