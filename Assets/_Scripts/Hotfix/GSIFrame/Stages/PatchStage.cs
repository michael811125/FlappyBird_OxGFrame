using Cysharp.Threading.Tasks;
using OxGFrame.AssetLoader;
using OxGFrame.CoreFrame;
using OxGFrame.GSIFrame;

namespace FlappyBird.Hotfix.Runtime
{
    /// <summary>
    /// Patch stage. Shows PatchUI and runs the YooAsset patch flow through
    /// AssetPatcher, moving on only once the whole patch is done.
    /// <para>
    /// 補丁階段。顯示 PatchUI 並透過 AssetPatcher 執行 YooAsset 的補丁流程,
    /// 待補丁全部完成後才切換至下一個階段。
    /// </para>
    /// </summary>
    public class PatchStage : GSIBase
    {
        // Execution order: OnCreate (once) > OnEnter (every change) > OnUpdate > OnExit
        // 執行順序: OnCreate (僅一次) > OnEnter (每次切換) > OnUpdate > OnExit

        /// <summary>
        /// Called once when the game stage is created.
        /// <para>遊戲階段建立時呼叫一次。</para>
        /// </summary>
        public async override UniTask OnCreate()
        {
            /* Do somethings OnCreate once in here */
            /* 在此處進行僅一次的 OnCreate 初始化 */
        }

        /// <summary>
        /// Called every time this game stage is entered.
        /// <para>每次進入此遊戲階段時呼叫。</para>
        /// </summary>
        public async override UniTask OnEnter()
        {
            // Show PatchUI
            // 顯示 PatchUI
            await CoreFrames.UIFrame.Show(Pkgs.BuiltinPkg, UIs.PatchUI, null, UIs.AwaitingUI, 0);

            // Start the patch check
            // 開始檢查補丁
            AssetPatcher.Check();
        }

        /// <summary>
        /// Called per frame while this game stage is running.
        /// <para>此遊戲階段運行期間每幀呼叫。</para>
        /// </summary>
        public override void OnUpdate(float dt = 0.0f)
        {
            if (AssetPatcher.IsDone())
            {
                this.StopUpdate();
                HGSIManager.ChangeStage<MainMenuStage>();
            }
        }

        /// <summary>
        /// Called when leaving this game stage.
        /// <para>離開此遊戲階段時呼叫。</para>
        /// </summary>
        public override void OnExit()
        {
            // Close all UIs
            // 關閉所有 UI
            CoreFrames.UIFrame.CloseAll(true, true);
        }
    }
}