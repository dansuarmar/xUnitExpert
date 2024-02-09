using System.Collections;
using ClassLibrary;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace TestProject.ClassLibraryTests;

public class CalculatorWithMemberAndClassData
{
    private readonly Calculator _sut = new();
    private readonly ITestOutputHelper _outputHelper;

    public CalculatorWithMemberAndClassData(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
        _outputHelper.WriteLine("This is the Setup");
    }

    public static IEnumerable<object[]> AddTestData => new List<object[]> 
    {
        new object[]{ 8, 4, 12 },
        new object[]{ 7, 5, 12 }, 
        new object[]{ 8, 5, 13 },
        new object[]{ 0, 0, 0 },
        new object[]{ -10, -11, -21 },
        new object[]{ 10, -11, -1 },
    };

    [Theory]
    [MemberData(nameof(AddTestData))]
    public void Add_ShouldAdd_WhenTwoInts(int a, int b, int expected)
    {
        //Arrange
        _outputHelper.WriteLine("This is the Add Test");

        //Act
        var result = _sut.Add(a, b);

        //Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [ClassData(typeof(SubstractTestData))]
    public void Subtract_ShouldSubstract_WhenTwoInts(int a, int b, int expected)
    {
        //Arrange
        _outputHelper.WriteLine("This is the Subtract Test");

        //Act
        var result = _sut.Subtract(a, b);

        //Assert
        Assert.Equal(expected, result);
    }
}

public class SubstractTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]{ 9, 2, 7 };
        yield return new object[]{ 10, -2, 12 };
        yield return new object[]{ -9, -2, -7 };
        yield return new object[]{ 15, 5, 10 };
        yield return new object[]{ 1, 0, 1 };
        yield return new object[]{ -1, 0, -1 };
        yield return new object[]{ 0, 0, 0 };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
