using Serilog;

namespace FOIBuraz.Configuration;

public static class ConfigureLogging
{
    public static void Configure()
    {
        string startupPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "logs/log.txt");

        Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(startupPath, outputTemplate: "{Timestamp: yyyy-MM-dd HH:mm} [{Level}] {Message}{NewLine}{Exception}")
                .CreateLogger();
    }
}