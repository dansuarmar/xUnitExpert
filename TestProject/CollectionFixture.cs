using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Xunit.Abstractions;

namespace TestProject;

[Collection("CollectionName")]
public class CollectionFixture_Test
{
    readonly ClassFixture4Collection _classFixture;
    private readonly ITestOutputHelper _testOutputHelper;
    readonly Guid Id;
    public CollectionFixture_Test(ITestOutputHelper testOutputHelper, ClassFixture4Collection classFixture)
    {
        _testOutputHelper = testOutputHelper;
        _classFixture = classFixture;
        Id = Guid.NewGuid();
    }

    [Fact]
    public void TestCollectionFixture11()
    {
        _testOutputHelper.WriteLine(_classFixture.Id.ToString());
        _testOutputHelper.WriteLine(Id.ToString());
        Id.Should().NotBe(_classFixture.Id);
    }

    [Fact]
    public void TestCollectionFixture12()
    {
        //This should be the same in both tests.
        _testOutputHelper.WriteLine(_classFixture.Id.ToString());
        //This should be different in both tests.
        _testOutputHelper.WriteLine(Id.ToString());
        Id.Should().NotBe(_classFixture.Id);
    }
}

[Collection("CollectionName")]
public class CollectionFixture2_Test
{
    readonly ClassFixture4Collection _classFixture;
    private readonly ITestOutputHelper _testOutputHelper;
    readonly Guid Id;
    public CollectionFixture2_Test(ITestOutputHelper testOutputHelper, ClassFixture4Collection classFixture)
    {
        _testOutputHelper = testOutputHelper;
        _classFixture = classFixture;
        Id = Guid.NewGuid();
    }

    [Fact]
    public void TestCollectionFixture21()
    {
        _testOutputHelper.WriteLine(_classFixture.Id.ToString());
        _testOutputHelper.WriteLine(Id.ToString());
        Id.Should().NotBe(_classFixture.Id);
    }

    [Fact]
    public void TestCollectionFixture22()
    {
        //This should be the same in both tests.
        _testOutputHelper.WriteLine(_classFixture.Id.ToString());
        //This should be different in both tests.
        _testOutputHelper.WriteLine(Id.ToString());
        Id.Should().NotBe(_classFixture.Id);
    }
}

public class ClassFixture4Collection
{
    public Guid Id = Guid.NewGuid();
}

[CollectionDefinition("CollectionName")]
public class CollectionFixture : ICollectionFixture<ClassFixture4Collection>{}