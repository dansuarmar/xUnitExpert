using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Xunit.Abstractions;

namespace TestProject;

public class ClassFixture_Test : IClassFixture<ClassFixture>
{
    readonly ClassFixture _classFixture;
    private readonly ITestOutputHelper _testOutputHelper;
    readonly Guid Id;
    public ClassFixture_Test(ITestOutputHelper testOutputHelper, ClassFixture classFixture)
    {
        _testOutputHelper = testOutputHelper;
        _classFixture = classFixture;
        Id = Guid.NewGuid();
    }

    [Fact]
    public void TestClassFixture1()
    {
        _testOutputHelper.WriteLine(_classFixture.Id.ToString());
        _testOutputHelper.WriteLine(Id.ToString());
        Id.Should().NotBe(_classFixture.Id);
    }

    [Fact]
    public void TestClassFixture2()
    {
        //This should be the same in both tests.
        _testOutputHelper.WriteLine(_classFixture.ToString());
        //This should be different in both tests.
        _testOutputHelper.WriteLine(Id.ToString());
        Id.Should().NotBe(_classFixture.Id);
    }
}

public class ClassFixture
{
    public Guid Id = Guid.NewGuid();
}