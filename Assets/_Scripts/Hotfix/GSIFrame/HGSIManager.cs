using OxGFrame.GSIFrame;

namespace FlappyBird.Hotfix.Runtime
{
    /// <summary>
    /// [Hotfix.Assembly] Game stage integration manager of the hotfix project.
    /// It owns every stage of the actual game, so all of them can be hot-updated.
    /// <para>
    /// [熱更工程] 熱更工程的遊戲階段整合管理器。
    /// 持有實際遊戲的所有階段, 因此這些階段都可以被熱更。
    /// </para>
    /// </summary>
    public class HGSIManager : GSIManagerBase<HGSIManager>
    {
        public HGSIManager()
        {
            // 4. Patch stage
            // 4. 補丁階段
            this.AddGameStage<PatchStage>();

            // 5. Main menu stage
            // 5. 主選單階段
            this.AddGameStage<MainMenuStage>();

            // 6. Gameplay stage
            // 6. 遊戲階段
            this.AddGameStage<GameplayStage>();
        }

        /// <summary>
        /// Called by the main MonoBehaviour Start
        /// <para>由主 MonoBehaviour 的 Start 呼叫</para>
        /// </summary>
        public override void OnStart()
        {
            this.ChangeGameStage<PatchStage>();
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