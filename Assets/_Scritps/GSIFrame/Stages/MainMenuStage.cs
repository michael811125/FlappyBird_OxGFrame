using Cysharp.Threading.Tasks;
using OxGFrame.GSIFrame;
using OxGFrame.MediaFrame;
using OxGFrame.CoreFrame;

public class MainMenuStage : GSIBase
{
    public enum MainMenuStep
    {
        INIT_SHOW,
        WAITING_FOR_PLAYER,
        START_GAME_PLAY
    }

    private MainMenuStep _step;

    public async override UniTask OnCreate()
    {
    }

    public async override UniTask OnEnter()
    {
        this._step = MainMenuStep.INIT_SHOW;
    }

    public override void OnUpdate(float dt = 0.0f)
    {
        /* Do Somethings Update in here */

        switch (this._step)
        {
            case MainMenuStep.INIT_SHOW:
                // 播放主選單 BGM
                MediaFrames.AudioFrame.Play(Audios.MenuBgm).Forget();

                // 開啟主選單場景
                CoreFrames.SRFrame.Show(SRs.MainMenuSR).Forget();

                // 開啟主選單 UI
                CoreFrames.UIFrame.Show(UIs.MainMenuUI).Forget();

                this._step = MainMenuStep.WAITING_FOR_PLAYER;
                break;

            case MainMenuStep.WAITING_FOR_PLAYER:
                break;

            case MainMenuStep.START_GAME_PLAY:
                // 關閉主選單 BGM
                MediaFrames.AudioFrame.Stop(Audios.MenuBgm);

                // 關閉 MainMenuSC
                CoreFrames.SRFrame.Close(SRs.MainMenuSR);

                // 關閉 MainMenuUI
                CoreFrames.UIFrame.Close(UIs.MainMenuUI);

                // 切換 GamePlay 階段
                GSIManager.ChangeStage<GamePlayStage>();
                break;
        }
    }

    /// <summary>
    /// 切換步驟
    /// </summary>
    /// <param name="step"></param>
    public void ChangeStep(MainMenuStep step)
    {
        this._step = step;
    }

    public override void OnExit()
    {
    }
}
