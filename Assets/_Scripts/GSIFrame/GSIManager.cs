using OxGFrame.GSIFrame;

/// <summary>
/// Game stage integration manager. Every game stage is registered in the
/// constructor, and OnStart decides which stage runs first.
/// It is driven by CoreSystem through DriveStart / DriveUpdate.
/// <para>
/// 遊戲階段整合管理器。所有遊戲階段都在建構子中註冊,
/// OnStart 則決定第一個執行的階段。
/// 由 CoreSystem 透過 DriveStart / DriveUpdate 驅動。
/// </para>
/// </summary>
public class GSIManager : GSIManagerBase<GSIManager>
{
    public GSIManager()
    {
        // 1. Startup stage
        // 1. 啟動階段
        this.AddGameStage<StartupStage>();

        // 2. Logo stage
        // 2. 商標階段
        this.AddGameStage<LogoStage>();

        // 3. Patch stage
        // 3. 補丁階段
        this.AddGameStage<PatchStage>();

        // 4. Main menu stage
        // 4. 主選單階段
        this.AddGameStage<MainMenuStage>();

        // 5. Gameplay stage
        // 5. 遊戲階段
        this.AddGameStage<GameplayStage>();
    }

    /// <summary>
    /// Called by the main MonoBehaviour Start
    /// <para>由主 MonoBehaviour 的 Start 呼叫</para>
    /// </summary>
    public override void OnStart()
    {
        // The first game stage (startup stage)
        // 首次遊戲階段 (啟動階段)
        this.ChangeGameStage<StartupStage>();
    }

    /// <summary>
    /// Called by the main MonoBehaviour Update
    /// <para>由主 MonoBehaviour 的 Update 呼叫</para>
    /// </summary>
    /// <param name="dt"></param>
    public override void OnUpdate(float dt = 0.0f)
    {
        base.OnUpdate(dt); // Must be called | 必須呼叫
    }
}

