using Cysharp.Threading.Tasks;
using OxGFrame.CoreFrame.GSFrame;
using OxGFrame.CoreFrame.UIFrame;
using OxGFrame.GSIFrame;

public class GamePlayStage : GameStageBase
{
    public enum GamePlayStep
    {
        INIT_GAME,
        WAITING_FOR_READY,
        START_GAME,
        PLAYING_GAME,
        GAMEOVER,
        DONE
    }

    private GamePlayStep _step;
    private bool _isStart = false;

    public GamePlayStage(byte gstId) : base(gstId)
    {
    }

    public override void ResetStage()
    {
        /* Do Somthing Reset in here */

        this._isStart = false;
    }

    public async override UniTask InitStage()
    {
        /* Do Somthing Init in here */

        this._step = GamePlayStep.INIT_GAME;
    }

    public override void UpdateStage(float dt = 0.0f)
    {
        /* Do Somthing Update in here */

        switch (this._step)
        {
            case GamePlayStep.INIT_GAME:

                // 重置分數
                CoreManager.ResetScore();

                // 開啟 GamePlay 場景
                GSManager.GetInstance().Show(AssetGroup.None, SCPath.GamePlaySC).Forget();

                // 開啟 GetReadyUI
                UIManager.GetInstance().Show(AssetGroup.None, UIPath.GetReadyUI).Forget();

                this._step = GamePlayStep.WAITING_FOR_READY;
                break;

            case GamePlayStep.WAITING_FOR_READY:
                break;

            case GamePlayStep.START_GAME:
                // 開啟 ScoreUI
                UIManager.GetInstance().Show(AssetGroup.None, UIPath.ScoreUI).Forget();

                // 標記遊戲開始
                this._isStart = true;

                this._step = GamePlayStep.PLAYING_GAME;
                break;

            case GamePlayStep.PLAYING_GAME:
                break;

            case GamePlayStep.GAMEOVER:
                // 開啟 SettlementUI
                UIManager.GetInstance().Show(AssetGroup.None, UIPath.SettlementUI).Forget();

                // 標記遊戲開始標記結束
                this._isStart = false;

                // 每次 GameOver 記錄分數
                CoreManager.SaveBestScore();

                this._step = GamePlayStep.DONE;
                break;

            case GamePlayStep.DONE:
                break;
        }
    }

    public override void ReleaseStage()
    {
        /* Do Somthing Release in here */

        // 判斷場景是否有開啟, 如果有開啟則關閉再開啟 (確保後續 Replay = reload)
        if (GSManager.GetInstance().CheckIsShowing(SCPath.GamePlaySC))
        {
            GSManager.GetInstance().Close(SCPath.GamePlaySC);
        }

        // 判斷 ScoreUI 是否開啟, 如果有開啟則進行關閉
        if (UIManager.GetInstance().CheckIsShowing(UIPath.ScoreUI))
        {
            UIManager.GetInstance().Close(UIPath.ScoreUI);
        }
    }

    /// <summary>
    /// 切換步驟
    /// </summary>
    /// <param name="step"></param>
    public void ChangeStep(GamePlayStep step)
    {
        this._step = step;
    }

    /// <summary>
    /// 遊戲是否開始
    /// </summary>
    /// <returns></returns>
    public bool IsGameStart()
    {
        return this._isStart;
    }
}
