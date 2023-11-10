using Cysharp.Threading.Tasks;
using OxGFrame.CoreFrame;
using OxGFrame.GSIFrame;

public class LogoStage : GSIBase
{
    public const string BuiltinPkg = "BuiltinPkg";
    public const string LogoUI = "LogoUI";

    public async override UniTask OnCreate()
    {
    }

    public async override UniTask OnEnter()
    {
        // Show LogoUI (built-in)
        CoreFrames.UIFrame.Show(BuiltinPkg, LogoUI).Forget();
        // Do hotfix while logo showing
        GSIManager.ChangeStage<HotfixStage>();
    }

    public override void OnUpdate(float dt = 0.0f)
    {
    }

    public override void OnExit()
    {
    }
}
