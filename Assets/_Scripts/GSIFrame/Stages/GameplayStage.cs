using Cysharp.Threading.Tasks;
using OxGFrame.CoreFrame;
using OxGFrame.GSIFrame;

/// <summary>
/// Gameplay stage. Drives one run through its step machine (init, get ready,
/// playing, game over) and cleans the scene up on exit so a replay reloads it.
/// <para>
/// 遊戲階段。以步驟機驅動一局遊戲 (初始化、準備、遊玩、結束),
/// 離開時清理場景, 讓重新遊玩等同於重新載入。
/// </para>
/// </summary>
public class GameplayStage : GSIBase
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
        this._isStart = false;
        this._step = GamePlayStep.INIT_GAME;
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
            case GamePlayStep.INIT_GAME:
                // Reset the score
                // 重置分數
                CoreSystem.ResetScore();

                // Open the gameplay scene
                // 開啟 GamePlay 場景
                CoreFrames.SRFrame.Show(Pkgs.PatchPkg, SRs.GameplaySR).Forget();

                // Open GetReadyUI
                // 開啟 GetReadyUI
                CoreFrames.UIFrame.Show(Pkgs.PatchPkg, UIs.GetReadyUI, null, UIs.AwaitingUI, 0).Forget();

                this._step = GamePlayStep.WAITING_FOR_READY;
                break;

            case GamePlayStep.WAITING_FOR_READY:
                break;

            case GamePlayStep.START_GAME:
                // Open ScoreUI
                // 開啟 ScoreUI
                CoreFrames.UIFrame.Show(Pkgs.PatchPkg, UIs.ScoreUI).Forget();

                // Mark the gameplay as started
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
                // Open SettlementUI
                // 開啟 SettlementUI
                CoreFrames.UIFrame.Show(Pkgs.PatchPkg, UIs.SettlementUI, null, UIs.AwaitingUI, 0).Forget();

                // Clear the started mark
                // 標記遊戲開始標記結束
                this._isStart = false;

                // Record the score on every game over
                // 每次 GameOver 記錄分數
                CoreSystem.SaveBestScore();

                this._step = GamePlayStep.DONE;
                break;

            case GamePlayStep.DONE:
                break;
        }
    }

    /// <summary>
    /// Called when leaving this game stage.
    /// <para>離開此遊戲階段時呼叫。</para>
    /// </summary>
    public override void OnExit()
    {
        // If the scene is showing, close it so the next replay reloads it
        // 判斷場景是否有開啟, 如果有開啟則關閉再開啟 (確保後續 Replay = reload)
        if (CoreFrames.SRFrame.CheckIsShowing(SRs.GameplaySR))
        {
            CoreFrames.SRFrame.Close(SRs.GameplaySR);
        }

        // Close ScoreUI if it is showing
        // 判斷 ScoreUI 是否開啟, 如果有開啟則進行關閉
        if (CoreFrames.UIFrame.CheckIsShowing(UIs.ScoreUI))
        {
            CoreFrames.UIFrame.Close(UIs.ScoreUI);
        }
    }

    /// <summary>
    /// Change the step
    /// <para>切換步驟</para>
    /// </summary>
    /// <param name="step"></param>
    public void ChangeStep(GamePlayStep step)
    {
        this._step = step;
    }

    /// <summary>
    /// Whether the gameplay has started
    /// <para>遊戲是否開始</para>
    /// </summary>
    /// <returns></returns>
    public bool IsGameStart()
    {
        return this._isStart;
    }
}
