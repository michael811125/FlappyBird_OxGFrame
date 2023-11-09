using OxGFrame.GSIFrame;

public class GSIManager : GSIManagerBase<GSIManager>
{
    public GSIManager()
    {
        // 1. 啟動階段
        this.AddGameStage<StartupStage>();

        // 2. 商標階段
        this.AddGameStage<LogoStage>();

        // 3. 主選單階段
        this.AddGameStage<MainMenuStage>();

        // 4. 遊戲階段
        this.AddGameStage<GameplayStage>();
    }

    /// <summary>
    /// Call by Main MonoBehaviour Start
    /// </summary>
    public override void OnStart()
    {
        // 首次遊戲階段 (啟動階段)
        this.ChangeGameStage<StartupStage>();
    }

    /// <summary>
    /// Call by Main MonoBehaviour Update
    /// </summary>
    /// <param name="dt"></param>
    public override void OnUpdate(float dt = 0.0f)
    {
        base.OnUpdate(dt); // Must be called
    }
}

