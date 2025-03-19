using OxGKit.TimeSystem;
using UnityEngine;

namespace FlappyBird.Main.Runtime
{
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
            MGSIManager.DriveStart();
        }

        private void Update()
        {
            // Update GSI 
            MGSIManager.DriveUpdate(Time.deltaTime);
        }
    }
}