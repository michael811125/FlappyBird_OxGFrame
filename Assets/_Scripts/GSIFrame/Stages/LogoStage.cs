using Cysharp.Threading.Tasks;
using OxGFrame.CoreFrame;
using OxGFrame.GSIFrame;
using OxGKit.TimeSystem;

public class LogoStage : GSIBase
{
    public enum LogoStep
    {
        WAITING_FOR_LOGO,
        START_MAIN_MENU
    }

    private LogoStep _step;
    private RealTimer _realTimer;

    public async override UniTask OnCreate()
    {
        this._realTimer = new RealTimer();
    }

    public async override UniTask OnEnter()
    {
        // 顯示 LogoUI
        await CoreFrames.UIFrame.Show(Pkgs.BuiltinPkg, UIs.LogoUI);

        // 設定 LogoUI 動畫的緩衝時間
        this._realTimer.Reset();
        this._realTimer.Play();
        this._realTimer.SetTimer(2f);

        this._step = LogoStep.WAITING_FOR_LOGO;
    }

    public override void OnUpdate(float dt = 0.0f)
    {
        switch (this._step)
        {
            case LogoStep.WAITING_FOR_LOGO:
                if (this._realTimer.IsTimerTimeout())
                {
                    // 關閉 LogoUI
                    CoreFrames.UIFrame.Close(UIs.LogoUI);
                    // 設定 LogoUI 結束動畫的緩衝時間
                    this._realTimer.SetTimer(1f);
                    // 切換下一步
                    this._step = LogoStep.START_MAIN_MENU;
                }
                break;

            case LogoStep.START_MAIN_MENU:
                if (this._realTimer.IsTimerTimeout())
                {
                    // 切換至 Patch 階段
                    GSIManager.ChangeStage<PatchStage>();
                }
                break;
        }
    }

    public override void OnExit()
    {
    }
}
