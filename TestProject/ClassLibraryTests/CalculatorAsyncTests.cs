using ClassLibrary;
using Xunit.Abstractions;

namespace TestProject.ClassLibraryTests;

public class CalculatorAsyncTests : IAsyncLifetime
{
    private readonly Calculator _sut = new();
    private readonly ITestOutputHelper _outputHelper;

    public CalculatorAsyncTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
        _outputHelper.WriteLine("This is the Setup from ctor");
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

    //Async Initialization
    public async Task InitializeAsync()
    {
        _outputHelper.WriteLine("This is the Setup from InitializeAsync");
        await Task.Delay(1);
    }

    //Async Cleanup
    public async Task DisposeAsync()
    {
        _outputHelper.WriteLine("This is the Cleanup from DisposeAsync");
        await Task.Delay(1);
    }
}