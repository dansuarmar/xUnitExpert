using ClassLibrary;
using Xunit.Abstractions;

namespace TestProject;

public class Calculator_Tests : IDisposable
{
    private readonly Calculator _sut = new();
    private readonly ITestOutputHelper _outputHelper;

    public Calculator_Tests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
        _outputHelper.WriteLine("This is the Setup");
    }

    [Fact]
    public void Add_ShouldAdd_WhenTwoInts()
    {
        //Arrange
        //Act
        var result = _sut.Add(7, 9);

        //Assert
        Assert.Equal(16, result);
    }

    public void Dispose()
    {
        _outputHelper.WriteLine("This is the Cleanup");
    }
}