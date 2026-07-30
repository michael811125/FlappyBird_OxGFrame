using OxGKit.LoggingSystem;

namespace FlappyBird.Hotfix.Runtime
{
    [LoggerName("FlappyBird.Hotfix.Logger")]
    public class HLogger : Logging
    {
        public HLogger() { }
    }

    /// <summary>
    /// Logger registration helper for the hotfix project.
    /// <para>
    /// 熱更工程的 Logger 註冊輔助類別。
    /// </para>
    /// </summary>
    public static class Loggers
    {
        public static void InitLoggers()
        {
            LoggingLauncher.CreateLogger<HLogger>();
            LoggingLauncher.TryLoadLoggers();
        }
    }
}