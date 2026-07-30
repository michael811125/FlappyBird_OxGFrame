using Cysharp.Threading.Tasks;
using OxGFrame.CoreFrame;
using OxGFrame.GSIFrame;
using OxGKit.TimeSystem;

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
    public enum LogoStep
    {
        WAITING_FOR_LOGO,
        START_MAIN_MENU
    }

    private LogoStep _step;
    private RealTimer _realTimer;

    /// <summary>
    /// Called once when the game stage is created.
    /// <para>遊戲階段建立時呼叫一次。</para>
    /// </summary>
    public async override UniTask OnCreate()
    {
        this._realTimer = new RealTimer();
    }

    /// <summary>
    /// Called every time this game stage is entered.
    /// <para>每次進入此遊戲階段時呼叫。</para>
    /// </summary>
    public async override UniTask OnEnter()
    {
        // Show LogoUI
        // 顯示 LogoUI
        await CoreFrames.UIFrame.Show(UIs.LogoUI);

        // Set the buffer time for the LogoUI intro animation
        // 設定 LogoUI 動畫的緩衝時間
        this._realTimer.Reset();
        this._realTimer.Play();
        this._realTimer.SetTimer(2f);

        this._step = LogoStep.WAITING_FOR_LOGO;
    }

    /// <summary>
    /// Called per frame while this game stage is running.
    /// <para>此遊戲階段運行期間每幀呼叫。</para>
    /// </summary>
    public override void OnUpdate(float dt = 0.0f)
    {
        switch (this._step)
        {
            case LogoStep.WAITING_FOR_LOGO:
                if (this._realTimer.IsTimerTimeout())
                {
                    // Close LogoUI
                    // 關閉 LogoUI
                    CoreFrames.UIFrame.Close(UIs.LogoUI);
                    // Set the buffer time for the LogoUI outro animation
                    // 設定 LogoUI 結束動畫的緩衝時間
                    this._realTimer.SetTimer(1f);
                    // Change to the next step
                    // 切換下一步
                    this._step = LogoStep.START_MAIN_MENU;
                }
                break;

            case LogoStep.START_MAIN_MENU:
                if (this._realTimer.IsTimerTimeout())
                {
                    // Change to the patch stage
                    // 切換至 Patch 階段
                    GSIManager.ChangeStage<MainMenuStage>();
                }
                break;
        }
    }

    /// <summary>
    /// Called when leaving this game stage.
    /// <para>離開此遊戲階段時呼叫。</para>
    /// </summary>
    public override void OnExit()
    {
    }
}