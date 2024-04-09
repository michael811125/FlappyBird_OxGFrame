using OxGKit.LoggingSystem;
using OxGKit.Utilities.Timer;
using UnityEngine;

/// <summary>
/// Main CoreSystem
/// </summary>
public class CoreSystem : MonoBehaviour
{
    private void Awake()
    {
        // Init loggers
        this._InitLoggers();

        // Instance AOT Generic
        RefTypes refTypes = new RefTypes();

        // Init RealTime at startup time
        RealTime.InitStartupTime();
    }

    private void Start()
    {
        // Run first stage on GSI start
        GSIManager.Start();
    }

    private void Update()
    {
        // Update GSI 
        GSIManager.Update(Time.deltaTime);
    }

    private void _InitLoggers()
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
