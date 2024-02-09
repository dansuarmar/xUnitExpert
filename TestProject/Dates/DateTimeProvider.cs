namespace TestProject;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset Now()
    {
        return DateTimeOffset.Now;
    }

    public DateTimeOffset UtcNow()
    {
        return DateTimeOffset.UtcNow;
    }
}

public interface IDateTimeProvider
{
    public DateTimeOffset Now();
    public DateTimeOffset UtcNow();
}
