using OxGKit.TimeSystem;
using UnityEngine;

namespace FlappyBird.Main.Runtime
{
    /// <summary>
    /// [Main.Assembly] The main program of the main (AOT) project.
    /// It lives in the Main scene, drives MGSIManager, and is responsible for getting
    /// the hotfix assemblies loaded. Once the hotfix is done the flow hands over to
    /// HCoreSystem in the hotfix main scene.
    /// <para>
    /// [主工程] 主工程 (AOT) 的主程序。
    /// 掛載於 Main 場景, 驅動 MGSIManager, 並負責讓熱更程序集載入完成。
    /// 熱更完成後, 流程即交棒給熱更主場景中的 HCoreSystem。
    /// </para>
    /// </summary>
    public class MCoreSystem : MonoBehaviour
    {
        private void Awake()
        {
            // Init loggers
            // 初始化 Logger
            Loggers.InitLoggers();

            // Instance the AOT generic types so HybridCLR keeps them alive
            // 實例化 AOT 泛型, 讓 HybridCLR 保留這些型別
            RefTypes refTypes = new RefTypes();

            // Init RealTime at startup time
            // 於啟動時初始化 RealTime
            RealTime.InitStartupTime();
        }

        private void Start()
        {
            // Run the first stage on GSI start
            // GSI 啟動時執行第一個階段
            MGSIManager.DriveStart();
        }

        private void Update()
        {
            // Update GSI
            // 更新 GSI
            MGSIManager.DriveUpdate(Time.deltaTime);
        }
    }
}