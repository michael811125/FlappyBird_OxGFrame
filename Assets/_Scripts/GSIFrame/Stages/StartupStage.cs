using Cysharp.Threading.Tasks;
using OxGFrame.AssetLoader;
using OxGFrame.GSIFrame;

public class StartupStage : GSIBase
{
    // Execution Order: OnInit (Once) > OnEnter (Every Change) > OnUpdate

    public async override UniTask OnCreate()
    {
        /* Do Somethings OnCreate once in here */
    }

    public async override UniTask OnEnter()
    {

    }

    public override void OnUpdate(float dt = 0.0f)
    {
        // 輪詢 Bundle 相關配置是否已經初始完畢
        if (AssetPatcher.IsInitialized())
        {
            GSIManager.ChangeStage<LogoStage>();
        }
    }

    public override void OnExit()
    {
        /* Do Somethings OnExit in here */
    }
}
