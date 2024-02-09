namespace TestProject;

public class Greeting
{
    IDateTimeProvider _dtProvider;

    public Greeting(IDateTimeProvider dtProvider)
    {
        _dtProvider = dtProvider;
    }

    public string GetnerateGreet()
    {
        var now = _dtProvider.Now();
        return now.Hour switch{
            >= 5 and < 12 => "Good Morning",
            >= 12 and < 18 => "Good Afternoon",
            _ => "Good Evening",
        };
    }
}
