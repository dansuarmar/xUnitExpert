using FluentAssertions;
using NSubstitute;

namespace TestProject;

public class GreetingTests
{
    private readonly Greeting _sut;
    private readonly IDateTimeProvider _dtProvider;

    public GreetingTests()
    {
        _dtProvider = Substitute.For<IDateTimeProvider>();
        _sut = new Greeting(_dtProvider);
    }

    [Fact]
    public void GetnerateGreet_ShouldReturnGoodMorning_WhenIsMornind()
    {
        //Arrange
        _dtProvider.Now().Returns(new DateTime(2023, 10, 10, 8, 0, 0));

        //Act
        var response = _sut.GetnerateGreet();

        //Assert
        response.Should().Be("Good Morning");
    }
    
    [Fact]
    public void GetnerateGreet_ShouldReturnGoodAfternoon_WhenIsAfternoon()
    {
        //Arrange
        _dtProvider.Now().Returns(new DateTime(2023, 10, 10, 15, 0, 0));

        //Act
        var response = _sut.GetnerateGreet();

        //Assert
        response.Should().Be("Good Afternoon");
    }
    
    [Fact]
    public void GetnerateGreet_ShouldReturnGoodMEvenint_WhenIsEvening()
    {
        //Arrange
        _dtProvider.Now().Returns(new DateTime(2023, 10, 10, 20, 0, 0));

        //Act
        var response = _sut.GetnerateGreet();

        //Assert
        response.Should().Be("Good Evening");
    }
}
