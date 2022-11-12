using Cysharp.Threading.Tasks;
using OxGFrame.GSIFrame;
using OxGFrame.CoreFrame.GSFrame;
using OxGFrame.CoreFrame.UIFrame;
using OxGFrame.MediaFrame.AudioFrame;
using UnityEngine;

public class MainMenuStage : GameStageBase
{
    public enum MainMenuStep
    {
        INIT_SHOW,
        WAITING_FOR_PLAYER,
        START_GAME_PLAY
    }

    private MainMenuStep _step;

    public MainMenuStage(byte gstId) : base(gstId)
    {
    }

    public override void ResetStage()
    {
        /* Do Somthing Reset in here */
    }

    public async override UniTask InitStage()
    {
        /* Do Somthing Init in here */

        this._step = MainMenuStep.INIT_SHOW;
    }

    public override void UpdateStage(float dt = 0.0f)
    {
        /* Do Somthing Update in here */

        switch (this._step)
        {
            case MainMenuStep.INIT_SHOW:
                // 播放主選單 BGM
                AudioManager.GetInstance().Play(AudioPath.MenuBgm).Forget();

                // 開啟主選單場景
                GSManager.GetInstance().Show(AssetGroup.None, SCPath.MainMenuSC).Forget();

                // 開啟主選單 UI
                UIManager.GetInstance().Show(AssetGroup.None, UIPath.MainMenuUI).Forget();

                this._step = MainMenuStep.WAITING_FOR_PLAYER;
                break;

            case MainMenuStep.WAITING_FOR_PLAYER:
                break;

            case MainMenuStep.START_GAME_PLAY:
                // 關閉主選單 BGM
                AudioManager.GetInstance().Stop(AudioPath.MenuBgm);

                // 關閉 MenuBackgroundSC
                GSManager.GetInstance().Close(SCPath.MainMenuSC);

                // 關閉 MainMenuUI
                UIManager.GetInstance().Close(UIPath.MainMenuUI);

                // 切換 GamePlay 階段
                GSM.GetInstance().ChangeGameStage(GSM.GAME_PLAY_STAGE_ID);
                break;
        }
    }

    public override void ReleaseStage()
    {
        /* Do Somthing Release in here */
    }

    /// <summary>
    /// 切換步驟
    /// </summary>
    /// <param name="step"></param>
    public void ChangeStep(MainMenuStep step)
    {
        this._step = step;
    }
}
