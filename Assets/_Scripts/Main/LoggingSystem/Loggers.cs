using OxGKit.LoggingSystem;

[LoggerName("FlappyBird.Main.Logger")]
public class MLogger : Logging
{
    public MLogger() { }
}

public static class Loggers
{
    public static void InitLoggers()
    {
        Logging.CreateLogger<OxGFrame.Hotfixer.Logger>();
        Logging.CreateLogger<OxGFrame.AssetLoader.Logger>();
        Logging.CreateLogger<OxGFrame.CoreFrame.Logger>();
        Logging.CreateLogger<OxGFrame.MediaFrame.Logger>();
        Logging.CreateLogger<OxGFrame.GSIFrame.Logger>();
        Logging.CreateLogger<OxGFrame.CenterFrame.Logger>();
        Logging.CreateLogger<OxGKit.Utilities.Logger>();
        Logging.CreateLogger<MLogger>();
        LoggingLauncher.TryLoadLoggerSetting();
    }
}