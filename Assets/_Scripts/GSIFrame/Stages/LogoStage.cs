using Cysharp.Threading.Tasks;
using OxGFrame.CoreFrame;
using OxGFrame.GSIFrame;
using OxGKit.Utilities.Timer;

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
        // Show LogoUI
        await CoreFrames.UIFrame.Show(UIs.LogoUI);

        // Set buffer timer for Logo
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
                    // Close LogoUI
                    CoreFrames.UIFrame.Close(UIs.LogoUI);
                    // Set close LogoUI buffer time
                    this._realTimer.SetTimer(1f);
                    // Change step
                    this._step = LogoStep.START_MAIN_MENU;
                }
                break;

            case LogoStep.START_MAIN_MENU:
                if (this._realTimer.IsTimerTimeout())
                {
                    // Change to MainMenuStage
                    GSIManager.ChangeStage<MainMenuStage>();
                }
                break;
        }
    }

    public override void OnExit()
    {
    }
}
