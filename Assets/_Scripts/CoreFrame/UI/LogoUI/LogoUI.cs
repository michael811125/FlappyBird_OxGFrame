using Cysharp.Threading.Tasks;
using OxGFrame.CoreFrame.UIFrame;
using OxGKit.Utilities.EasyAnim;

public class LogoUI : UIBase
{
    // Use _Node@XXX to Bind

    #region Binding Components
    protected EasyAnimation _transitionEasyAnim;

    /// <summary>
    /// Auto Binding Section
    /// </summary>
    protected override void OnAutoBind()
    {
        base.OnAutoBind();
        this._transitionEasyAnim = this.collector.GetNodeComponent<EasyAnimation>("Transition*EasyAnim");
    }
    #endregion

    public override void OnCreate()
    {
        /**
         * Do Somethings Init Once In Here
         */
    }

    protected override async UniTask OnPreShow()
    {
        /**
         * Open Sub With Async
         */
    }

    protected override void OnPreClose()
    {
        /**
         * Close Sub
         */
    }

    protected override void OnBind()
    {
    }

    protected override void OnShow(object obj)
    {
        /**
         * Do Somethings Init With Every Showing In Here
         */
    }

    protected override void OnUpdate(float dt)
    {
        /**
         * Do Update Per FrameRate
         */
    }

    public override void OnReceiveAndRefresh(object obj = null)
    {
        /**
         * Do Refresh Once After Data Receive
         */
    }

    protected override void OnShowAnimation(AnimationEnd animationEnd)
    {
        this._transitionEasyAnim.Play("Intro", () => { animationEnd(); });
    }

    protected override void OnCloseAnimation(AnimationEnd animationEnd)
    {
        this._transitionEasyAnim.Play("Outro", () => { animationEnd(); });
    }

    protected override void OnClose()
    {
        /**
         * Do Somethings on close (Close)
         */
    }

    public override void OnRelease()
    {
        /**
         * Do Somethings on release (CloseAndDestroy)
         */
    }
}
