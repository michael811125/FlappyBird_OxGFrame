using OxGKit.LoggingSystem;

namespace FlappyBird.Hotfix.Runtime
{
    [LoggerName("FlappyBird.Hotfix.Logger")]
    public class HLogger : Logging
    {
        public HLogger() { }
    }

    public static class Loggers
    {
        public static void InitLoggers()
        {
            Logging.CreateLogger<HLogger>();
            LoggingLauncher.TryLoadLoggerSetting();
        }
    }
}