using Cysharp.Threading.Tasks;
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
        GSIManager.ChangeStage<LogoStage>();
    }

    public override void OnUpdate(float dt = 0.0f)
    {
    }

    public override void OnExit()
    {
        /* Do Somethings OnExit in here */
    }
}
