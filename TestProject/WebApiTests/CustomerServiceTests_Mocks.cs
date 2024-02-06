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
        public async Task GetAllAsync_ShouldReturnEmpty_WhenNoCustomerExist()
        {
            //Arrange
            _repository.GetAllAsync().Returns(Array.Empty<Customer>());

            //Act
            var customers = await _sut.GetAllAsync();

            //Assert
            customers.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnCustomers_WhenExists()
        {
            var customer = new Customer
            {
                Name = "name",
                Id = 1,
                CreatedDate = DateTime.Now,
                Credit = 1000m,
            };

            //Arrange
            var array = new[]{
                customer,
            };
            _repository.GetAllAsync().Returns(array);

            //Act
            var customers = await _sut.GetAllAsync();

            //Assert
            customers.Should().NotBeEmpty();
            customers.Should().HaveCount(1);
            customers.Should().ContainSingle().Equals(customer);
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
        public async Task GetAllAsync_ShouldLogException_WhenExceptionIsThrown()
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

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNoCustomerExist()
        {
            //Arrange
            Customer? customer = null;
            _repository.GetByIdAsync(4).Returns(customer);

            //Act
            var customers = await _sut.GetByIdAsync(4);

            //Assert
            customers.Should().BeNull();
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCustomer_WhenExists()
        {
            var customer = new Customer
            {
                Name = "name",
                Id = 1,
                CreatedDate = DateTime.Now,
                Credit = 1000m,
            };

            //Arrange
            _repository.GetByIdAsync(1).Returns(customer);

            //Act
            var customerResponse = await _sut.GetByIdAsync(1);

            //Assert
            customerResponse.Should().NotBeNull();
            customerResponse?.Name.Should().Be("name");
        }

        [Fact]
        public async Task GetByIdAsync_ShouldLogMessages_WhenInoked()
        {
            //Arrange
            var id = 1;
            Customer? customer = null;
            _repository.GetByIdAsync(id).Returns(customer);

            //Act
            _ = await _sut.GetByIdAsync(id);

            //Assert
            _logger.Received(1).LogInformation("Retriving customer with Id {0}", Arg.Any<int>()); //This 2 calls do exactly the same validation.
            _logger.Received(1).LogInformation(Arg.Is("Customer with Id {1} retrived in {0}ms"), Arg.Any<long>(), id);
        }

        [Fact]
        public async Task GetBytIdAsync_ShouldLogException_WhenExceptionIsThrown()
        {
            //Arrange
            var id = 1;
            var exception = new Exception("Test Exception");
            _repository.GetByIdAsync(1).Throws(exception);

            //Act
            var result = async () => await _sut.GetByIdAsync(id);

            await result.Should().ThrowAsync<Exception>().WithMessage("Test Exception");
            _logger.Received(1).LogError(Arg.Is(exception), Arg.Is("Something went wrong when retriving customer with Id {0}."), id);
        }

        [Fact]
        public async Task AddAsync_ShouldReturnCustomer_WhenCreated()
        {
            //Arrange
            var customer = new Customer
            {
                Name = "name",
                Id = 1,
                CreatedDate = DateTime.Now,
                Credit = 1000m,
            };
            _repository.Add(customer).Returns(customer);

            //Act
            var customerResponse = await _sut.AddAsync(customer);

            //Assert
            customerResponse.Should().NotBeNull();
            customerResponse?.Name.Should().Be("name");
        }

        [Fact]
        public async Task AddAsync_ShouldLogMessages_WhenInoked()
        {
            //Arrange
            var customer = new Customer
            {
                Name = "name",
                Id = 1,
                CreatedDate = DateTime.Now,
                Credit = 1000m,
            };
            _repository.Add(customer).Returns(customer);

            //Act
            _ = await _sut.AddAsync(customer);

            //Assert
            _logger.Received(1).LogInformation("Creating new customer with id {0}", Arg.Any<int>());
            _logger.Received(1).LogInformation(Arg.Is("Customer with Id {1} created in {0}ms"), Arg.Any<long>(), Arg.Any<int>());
        }

        [Fact]
        public async Task AddAsync_ShouldLogException_WhenExceptionIsThrown()
        {
            //Arrange
            var customer = new Customer
            {
                Name = "name",
                Id = 1,
                CreatedDate = DateTime.Now,
                Credit = 1000m,
            };
            var exception = new Exception("Test Exception");
            _repository.Add(customer).Throws(exception);

            //Act
            var result = async () => await _sut.AddAsync(customer);

            await result.Should().ThrowAsync<Exception>().WithMessage("Test Exception");
            _logger.Received(1).LogError(Arg.Is(exception), Arg.Is("Something went wrong when creating customer with id {0}."), customer.Id);
        }
    }
}
