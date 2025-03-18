using OxGFrame.GSIFrame;

namespace FlappyBird.Main.Runtime
{
    public class MGSIManager : GSIManagerBase<MGSIManager>
    {
        /// <summary>
        /// Main GSIManager
        /// </summary>
        public MGSIManager()
        {
            // 1. Game Startup Stage
            this.AddGameStage<StartupStage>();

            // 2. Game Logo Stage
            this.AddGameStage<LogoStage>();

            // 3. Game Hotfix Stage
            this.AddGameStage<HotfixStage>();
        }

        /// <summary>
        /// Call by Main MonoBehaviour Start
        /// </summary>
        public override void OnStart()
        {
            this.ChangeGameStage<StartupStage>();
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
}