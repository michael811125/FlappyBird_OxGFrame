using OxGKit.LoggingSystem;

namespace FlappyBird.Main.Runtime
{
    [LoggerName("FlappyBird.Main.Logger")]
    public class MLogger : Logging
    {
        public MLogger() { }
    }

    public static class Loggers
    {
        public static void InitLoggers()
        {
            LoggingLauncher.CreateLogger<OxGFrame.Hotfixer.Logger>();
            LoggingLauncher.CreateLogger<OxGFrame.AssetLoader.Logger>();
            LoggingLauncher.CreateLogger<OxGFrame.CoreFrame.Logger>();
            LoggingLauncher.CreateLogger<OxGFrame.MediaFrame.Logger>();
            LoggingLauncher.CreateLogger<OxGFrame.GSIFrame.Logger>();
            LoggingLauncher.CreateLogger<OxGFrame.CenterFrame.Logger>();
            LoggingLauncher.CreateLogger<OxGKit.Utilities.Logger>();
            LoggingLauncher.CreateLogger<MLogger>();
            LoggingLauncher.TryLoadLoggers();
        }
    }
}