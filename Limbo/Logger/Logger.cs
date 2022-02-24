using Serilog;

namespace AuthServerLimbo.Logger
{
    public class Logger
    {
        static readonly Serilog.Core.Logger ProgramLogger = new LoggerConfiguration()
        .WriteTo.Console()
        .CreateLogger();

        public static void Log(string text) => ProgramLogger.Information(text);
        public static void Warn(string text) => ProgramLogger.Warning(text);
        //public static void ErrorLog(string text) => ProgramLogger.Error(text);
        //public static void FatalLog(string text) => ProgramLogger.Fatal(text);

        public static void DisposeLogger() => ProgramLogger.Dispose();
    }
}
