using Serilog;
using System.IO;
using System.Reflection;

namespace AliaksNad.Battleship.Logic.Profiles
{
    public class LoggerProfile : LoggerConfiguration
    {
        public LoggerProfile()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            WriteTo.Debug();
            WriteTo.Console();
            WriteTo.Async(x => x.File(Path.Combine(path, "Logs/Log.txt"), rollOnFileSizeLimit: true, fileSizeLimitBytes: 10_000_000, rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information));
            WriteTo.File(Path.Combine(path, "Logs/DebugLog.txt"), rollOnFileSizeLimit: true, fileSizeLimitBytes: 10_000_000, rollingInterval: RollingInterval.Day);
            Enrich.WithHttpRequestType();
            Enrich.WithWebApiControllerName();
            Enrich.WithWebApiActionName();
            MinimumLevel.Debug();
        }
    }
}
