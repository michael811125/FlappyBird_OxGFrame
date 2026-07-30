using Cysharp.Threading.Tasks;
using OxGFrame.GSIFrame;
using OxGFrame.MediaFrame;
using OxGFrame.CoreFrame;

namespace FlappyBird.Hotfix.Runtime
{
    /// <summary>
    /// Main menu stage. A small step machine: open the menu scene and UI, wait for the
    /// player, then tear them down and change to the gameplay stage.
    /// <para>
    /// 主選單階段。以簡單的步驟機運作：開啟主選單場景與 UI、等待玩家操作,
    /// 再關閉兩者並切換至遊戲階段。
    /// </para>
    /// </summary>
    public class MainMenuStage : GSIBase
    {
        public enum MainMenuStep
        {
            INIT_SHOW,
            WAITING_FOR_PLAYER,
            START_GAME_PLAY
        }

        private MainMenuStep _step;

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
            this._step = MainMenuStep.INIT_SHOW;
        }

        /// <summary>
        /// Called per frame while this game stage is running.
        /// <para>此遊戲階段運行期間每幀呼叫。</para>
        /// </summary>
        public override void OnUpdate(float dt = 0.0f)
        {
            /* Do somethings update in here */
            /* 在此處進行 Update 的處理 */

            switch (this._step)
            {
                case MainMenuStep.INIT_SHOW:
                    // Play the main menu BGM
                    // 播放主選單 BGM
                    MediaFrames.AudioFrame.Play(Pkgs.PatchPkg, Audios.MenuBgm).Forget();

                    // Open the main menu scene
                    // 開啟主選單場景
                    CoreFrames.SRFrame.Show(Pkgs.PatchPkg, SRs.MainMenuSR).Forget();

                    // Open the main menu UI
                    // 開啟主選單 UI
                    CoreFrames.UIFrame.Show(Pkgs.PatchPkg, UIs.MainMenuUI, null, UIs.AwaitingUI, 0).Forget();

                    this._step = MainMenuStep.WAITING_FOR_PLAYER;
                    break;

                case MainMenuStep.WAITING_FOR_PLAYER:
                    break;

                case MainMenuStep.START_GAME_PLAY:
                    // Stop the main menu BGM
                    // 關閉主選單 BGM
                    MediaFrames.AudioFrame.Stop(Audios.MenuBgm);

                    // Close MainMenuSR
                    // 關閉 MainMenuSC
                    CoreFrames.SRFrame.Close(SRs.MainMenuSR);

                    // Close MainMenuUI
                    // 關閉 MainMenuUI
                    CoreFrames.UIFrame.Close(UIs.MainMenuUI);

                    // Change to the gameplay stage
                    // 切換 GamePlay 階段
                    HGSIManager.ChangeStage<GameplayStage>();
                    break;
            }
        }

        /// <summary>
        /// Change the step
        /// <para>切換步驟</para>
        /// </summary>
        /// <param name="step"></param>
        public void ChangeStep(MainMenuStep step)
        {
            this._step = step;
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