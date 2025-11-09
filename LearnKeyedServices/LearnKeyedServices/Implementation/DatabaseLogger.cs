using LearnKeyedServices.Interfaces;

namespace LearnKeyedServices.Implementation;

/*
 * FileLogger class implements the ICustomLogger interface.
 * When I first started writing this, I used the ILogger interface from Microsoft.Extensions.Logging.
 * ILogger has 3 methods: Log, IsEnabled, and BeginScope. I realized my mistake, deleted that code, and 
 * created my own interface ICustomLogger with only the Log method, following Kanjilal code.
 */
public class DatabaseLogger : ICustomLogger
{
    public void Log(string message)
    {
        // Simulate logging to a database by writing to the console
        Console.WriteLine($"[DatabaseLogger] {DateTime.Now}: {message}");
    }
}
