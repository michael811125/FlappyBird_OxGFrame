using Cysharp.Threading.Tasks;
using OxGFrame.CoreFrame;
using OxGFrame.GSIFrame;

namespace FlappyBird.Main.Runtime
{
    /// <summary>
    /// Logo stage. Shows LogoUI, waits out the intro and outro animation with a
    /// RealTimer, then moves on to the next stage.
    /// <para>
    /// 商標階段。顯示 LogoUI, 以 RealTimer 等待進場與出場動畫,
    /// 結束後再切換至下一個階段。
    /// </para>
    /// </summary>
    public class LogoStage : GSIBase
    {
        public const string BuiltinPkg = "BuiltinPkg";
        public const string LogoUI = "LogoUI";

        /// <summary>
        /// Called once when the game stage is created.
        /// <para>遊戲階段建立時呼叫一次。</para>
        /// </summary>
        public async override UniTask OnCreate()
        {
        }

        /// <summary>
        /// Called every time this game stage is entered.
        /// <para>每次進入此遊戲階段時呼叫。</para>
        /// </summary>
        public async override UniTask OnEnter()
        {
            // Show LogoUI (from the built-in package)
            // 顯示 LogoUI (來自內建 Package)
            CoreFrames.UIFrame.Show(BuiltinPkg, LogoUI).Forget();
            // Do the hotfix while the logo is showing
            // 在顯示商標的同時進行熱更
            MGSIManager.ChangeStage<HotfixStage>();
        }

        /// <summary>
        /// Called per frame while this game stage is running.
        /// <para>此遊戲階段運行期間每幀呼叫。</para>
        /// </summary>
        public override void OnUpdate(float dt = 0.0f)
        {
        }

        /// <summary>
        /// Called when leaving this game stage.
        /// <para>離開此遊戲階段時呼叫。</para>
        /// </summary>
        public override void OnExit()
        {
        }
    }
}