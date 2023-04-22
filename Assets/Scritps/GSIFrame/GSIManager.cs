using OxGFrame.GSIFrame;

public class GSIManager : GSIManagerBase<GSIManager>
{
    public GSIManager()
    {
        // 1. 主選單階段
        this.AddGameStage<MainMenuStage>();

        // 2. 遊戲階段
        this.AddGameStage<GamePlayStage>();
    }

    /// <summary>
    /// Call by Main MonoBehaviour Start
    /// </summary>
    public override void OnStart()
    {
        // 首次遊戲階段 (主選單階段)
        this.ChangeGameStage<MainMenuStage>();
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

