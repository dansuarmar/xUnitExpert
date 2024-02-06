using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NuGet.Protocol.Core.Types;
using WebApi;
using WebApi.Controllers;
using WebApi.Entities;
using WebApi.Services;

namespace TestProject;

public class CustomerControllerTests
{
    private readonly CustomerController _sut;
    private readonly ICustomerService _customerService = Substitute.For<ICustomerService>();
    private readonly ILoggerAdapter<CustomerController> _logger = Substitute.For<ILoggerAdapter<CustomerController>>();

    public CustomerControllerTests()
    {
        _sut = new CustomerController(_customerService);
    }

    [Fact]
    public async Task GetById_ShouldReturnOkAndObject_WhenCustomerExist() 
    {
        //Arrange
        var customer = new Customer() 
        {
            Id = 1,
            Name = "name",
            CreatedDate = DateTime.UtcNow,
            Credit = 1000,
        };
        _customerService.GetByIdAsync(customer.Id).Returns(customer);

        //Act
        var response = await _sut.Get(customer.Id);

        //Assert
   
     response.Should().BeOfType<OkObjectResult>();
        var responseOk = (OkObjectResult)response;
        responseOk.Value.Should().BeEquivalentTo(customer);
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenCustomerDontExist()
    {
        //Arrange
        _customerService.GetByIdAsync(Arg.Any<int>()).ReturnsNull();

        //Act
        var response = await _sut.Get(1);

        //Assert
        response.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task GetAll_ShouldReturnEmptyArray_WhenNoCustomerExist()
    {
        //Arrange
        _customerService.GetAllAsync().Returns(Enumerable.Empty<Customer>());

        //Act
        var response = await _sut.GetAll();

        //Assert
        response.Should().BeOfType<OkObjectResult>();
        var responseOk = (OkObjectResult)response;
        responseOk.Value.As<IEnumerable<Customer>>().Should().BeEmpty();
    }

    [Fact]
    public async Task GetAll_ShouldReturnCustomers_WhenCustomersExist()
    {
        //Arrange
        var customer = new Customer()
        {
            Id = 1,
            Name = "name",
            CreatedDate = DateTime.UtcNow,
            Credit = 1000,
        };
        var list = new List<Customer>() { customer };
        _customerService.GetAllAsync().Returns(list);

        //Act
        var response = await _sut.GetAll();

        //Assert
        response.Should().BeOfType<OkObjectResult>();
        var responseOk = (OkObjectResult)response;
        responseOk.Value.As<IEnumerable<Customer>>().Should().NotBeEmpty();
        responseOk.Value.As<IEnumerable<Customer>>().Should().BeEquivalentTo(list);
        responseOk.Value.As<IEnumerable<Customer>>().Should().ContainSingle(x => x == customer);
    }

    [Fact]
    public async Task Post_ShouldReturnCreatedAndCustomer_WhenCustomersWasCreated()
    {
        //Arrange
        var customer = new Customer()
        {
            Id = 1,
            Name = "name",
            CreatedDate = DateTime.UtcNow,
            Credit = 1000,
        };
        var list = new List<Customer>() { customer };
        //This forces the result for AddAsync to be a value you give. This helps when Ids are created in the Service or Repository.
        _customerService.AddAsync(Arg.Do<Customer>(x => customer = x)).Returns(customer);

        //Act
        var response = await _sut.Post(customer);

        //Assert
        response.Should().BeOfType<CreatedAtActionResult>();
        var responseOk = (CreatedAtActionResult)response;
        responseOk.Value.As<Customer>().Should().BeEquivalentTo(customer);
        responseOk.RouteValues!["id"].Should().BeEquivalentTo(customer.Id);
    }

    [Fact]
    public async Task Post_ShouldReturnBadRequest_WhenCustomerCannotBeAdded()
    {
        //Arrange
        var customer = new Customer()
        {
            Id = 1,
            Name = "name",
            CreatedDate = DateTime.UtcNow,
            Credit = 1000,
        };
        //This forces the result for AddAsync to be a value you give. This helps when Ids are created in the Service or Repository.
        _customerService.AddAsync(Arg.Any<Customer>()).ReturnsNull();

        //Act
        var response = await _sut.Post(customer);

        //Assert
        response.Should().BeOfType<BadRequestResult>();
    }
}
