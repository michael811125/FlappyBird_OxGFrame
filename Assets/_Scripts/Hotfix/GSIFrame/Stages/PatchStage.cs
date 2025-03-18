using Cysharp.Threading.Tasks;
using OxGFrame.AssetLoader;
using OxGFrame.CoreFrame;
using OxGFrame.GSIFrame;

namespace FlappyBird.Hotfix.Runtime
{
    public class PatchStage : GSIBase
    {
        // Execution Order: OnInit (Once) > OnEnter (Every Change) > OnUpdate

        public async override UniTask OnCreate()
        {
            /* Do Somethings OnCreate once in here */
        }

        public async override UniTask OnEnter()
        {
            // Show PatchUI
            await CoreFrames.UIFrame.Show(Pkgs.BuiltinPkg, UIs.PatchUI, null, UIs.AwaitingUI, 0);

            // Check patch
            AssetPatcher.Check();
        }

        public override void OnUpdate(float dt = 0.0f)
        {
            if (AssetPatcher.IsDone())
            {
                this.StopUpdate();
                HGSIManager.ChangeStage<MainMenuStage>();
            }
        }

        public override void OnExit()
        {
            // Close all
            CoreFrames.UIFrame.CloseAll(true, true);
        }
    }
}