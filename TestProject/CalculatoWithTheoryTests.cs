using ClassLibrary;
using Xunit.Abstractions;

namespace TestProject;

public class CalculatorWithTheoryTests : IDisposable
{
    private readonly Calculator _sut = new();
    private readonly ITestOutputHelper _outputHelper;

    public CalculatorWithTheoryTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
        _outputHelper.WriteLine("This is the Setup");
    }

    [Theory]
    [InlineData(8,4,12)]
    [InlineData(7,5,12)]
    [InlineData(8,5,13)]
    [InlineData(0,0,0)]
    [InlineData(-10,-11,-21)]
    [InlineData(10,-11,-1)]
    public void Add_ShouldAdd_WhenTwoInts(int a, int b, int expected)
    {
        //Arrange
        _outputHelper.WriteLine("This is the Add Test");

        //Act
        var result = _sut.Add(a, b);

        //Assert
        Assert.Equal(expected, result);
    }
    
    [Theory(Skip = "Skiping this for test.")]
    [InlineData(9, 2, 7)]
    public void Subtract_ShouldSubstract_WhenTwoInts(int a, int b, int expected)
    {
        //Arrange
        _outputHelper.WriteLine("This is the Subtract Test");

        //Act
        var result = _sut.Subtract(a, b);

        //Assert
        Assert.Equal(expected, result);
    }

    public void Dispose()
    {
        _outputHelper.WriteLine("This is the Cleanup");
    }
}