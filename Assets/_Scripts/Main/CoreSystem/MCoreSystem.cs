using OxGKit.Utilities.Timer;
using UnityEngine;

/// <summary>
/// Main CoreSystem
/// </summary>
public class MCoreSystem : MonoBehaviour
{
    private void Awake()
    {
        // Init loggers
        Loggers.InitLoggers();

        // Instance AOT Generic
        RefTypes refTypes = new RefTypes();

        // Init RealTime at startup time
        RealTime.InitStartupTime();
    }

    private void Start()
    {
        // Run first stage on GSI start
        MGSIManager.Start();
    }

    private void Update()
    {
        // Update GSI 
        MGSIManager.Update(Time.deltaTime);
    }
}
