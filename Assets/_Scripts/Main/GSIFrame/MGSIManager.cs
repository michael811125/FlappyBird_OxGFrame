using OxGFrame.GSIFrame;

namespace FlappyBird.Main.Runtime
{
    /// <summary>
    /// [Main.Assembly] Game stage integration manager of the main project.
    /// It only owns the stages that must run before the hotfix is ready:
    /// startup, logo and hotfix.
    /// <para>
    /// [主工程] 主工程的遊戲階段整合管理器。
    /// 僅持有熱更完成前必須執行的階段：啟動、商標與熱更階段。
    /// </para>
    /// </summary>
    public class MGSIManager : GSIManagerBase<MGSIManager>
    {
        /// <summary>
        /// Main GSIManager
        /// <para>主工程的 GSIManager</para>
        /// </summary>
        public MGSIManager()
        {
            // 1. Game startup stage
            // 1. 遊戲啟動階段
            this.AddGameStage<StartupStage>();

            // 2. Game logo stage
            // 2. 遊戲商標階段
            this.AddGameStage<LogoStage>();

            // 3. Game hotfix stage
            // 3. 遊戲熱更階段
            this.AddGameStage<HotfixStage>();
        }

        /// <summary>
        /// Called by the main MonoBehaviour Start
        /// <para>由主 MonoBehaviour 的 Start 呼叫</para>
        /// </summary>
        public override void OnStart()
        {
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
}