using LearnKeyedServices.Interfaces;

namespace LearnKeyedServices.Implementation;

/*
 * FileLogger class implements the ICustomLogger interface.
 * When I first started writing this, I used the ILogger interface from Microsoft.Extensions.Logging.
 * ILogger has 3 methods: Log, IsEnabled, and BeginScope. I realized my mistake, deleted that code, and 
 * created my own interface ICustomLogger with only the Log method, following Kanjilal code.
 */
public class FileLogger : ICustomLogger
{
    public void Log(string message)
    {
        File.AppendAllText("log.txt", $"[FileLogger] {DateTime.Now}: {message}\n");
    }
}
