using Cysharp.Threading.Tasks;
using OxGFrame.CoreFrame;
using OxGFrame.GSIFrame;

public class GamePlayStage : GSIBase
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

    public async override UniTask OnCreate()
    {
    }

    public async override UniTask OnEnter()
    {
        this._isStart = false;
        this._step = GamePlayStep.INIT_GAME;
    }

    public override void OnUpdate(float dt = 0.0f)
    {
        /* Do Somethings Update in here */

        switch (this._step)
        {
            case GamePlayStep.INIT_GAME:
                // 重置分數
                CoreSystem.ResetScore();

                // 開啟 GamePlay 場景
                CoreFrames.SRFrame.Show(SRs.GamePlaySR).Forget();

                // 開啟 GetReadyUI
                CoreFrames.UIFrame.Show(UIs.GetReadyUI).Forget();

                this._step = GamePlayStep.WAITING_FOR_READY;
                break;

            case GamePlayStep.WAITING_FOR_READY:
                break;

            case GamePlayStep.START_GAME:
                // 開啟 ScoreUI
                CoreFrames.UIFrame.Show(UIs.ScoreUI).Forget();

                // 標記遊戲開始
                this._isStart = true;

                this._step = GamePlayStep.PLAYING_GAME;
                break;

            case GamePlayStep.PLAYING_GAME:
                /*
                 * 遊玩期間狀態會一直在此 Update
                 */
                break;

            case GamePlayStep.GAMEOVER:
                // 開啟 SettlementUI
                CoreFrames.UIFrame.Show(UIs.SettlementUI).Forget();

                // 標記遊戲開始標記結束
                this._isStart = false;

                // 每次 GameOver 記錄分數
                CoreSystem.SaveBestScore();

                this._step = GamePlayStep.DONE;
                break;

            case GamePlayStep.DONE:
                break;
        }
    }

    public override void OnExit()
    {
        // 判斷場景是否有開啟, 如果有開啟則關閉再開啟 (確保後續 Replay = reload)
        if (CoreFrames.SRFrame.CheckIsShowing(SRs.GamePlaySR))
        {
            CoreFrames.SRFrame.Close(SRs.GamePlaySR);
        }

        // 判斷 ScoreUI 是否開啟, 如果有開啟則進行關閉
        if (CoreFrames.UIFrame.CheckIsShowing(UIs.ScoreUI))
        {
            CoreFrames.UIFrame.Close(UIs.ScoreUI);
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
