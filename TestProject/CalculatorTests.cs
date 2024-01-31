using ClassLibrary;
using Xunit.Abstractions;

namespace TestProject;

public class CalculatorTests : IDisposable
{
    private readonly Calculator _sut = new();
    private readonly ITestOutputHelper _outputHelper;

    public CalculatorTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
        _outputHelper.WriteLine("This is the Setup");
    }

    [Fact]
    public void Add_ShouldAdd_WhenTwoInts()
    {
        //Arrange
        _outputHelper.WriteLine("This is the Add Test");

        //Act
        var result = _sut.Add(7, 9);

        //Assert
        Assert.Equal(16, result);
    }
    
    [Fact]
    public void Subtract_ShouldSubstract_WhenTwoInts()
    {
        //Arrange
        _outputHelper.WriteLine("This is the Subtract Test");

        //Act
        var result = _sut.Subtract(9, 2);

        //Assert
        Assert.Equal(7, result);
    }

    public void Dispose()
    {
        _outputHelper.WriteLine("This is the Cleanup");
    }
}