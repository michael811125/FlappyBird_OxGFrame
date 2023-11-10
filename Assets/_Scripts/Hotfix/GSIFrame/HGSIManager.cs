using OxGFrame.GSIFrame;

public class HGSIManager : GSIManagerBase<HGSIManager>
{
    public HGSIManager()
    {
        // 4. 補丁階段
        this.AddGameStage<PatchStage>();

        // 5. 主選單階段
        this.AddGameStage<MainMenuStage>();

        // 6. 遊戲階段
        this.AddGameStage<GameplayStage>();
    }

    /// <summary>
    /// Call by Main MonoBehaviour Start
    /// </summary>
    public override void OnStart()
    {
        this.ChangeGameStage<PatchStage>();
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

