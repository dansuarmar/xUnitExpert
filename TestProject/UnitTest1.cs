namespace TestProject;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        Console.WriteLine("Unit Test");
        Assert.Equal("", String.Empty);
    }
}