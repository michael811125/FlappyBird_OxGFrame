using OxGFrame.CoreFrame.UIFrame;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : UIBase
{
    // Use _Node@XXX to Bind

    #region Binding Components
    protected GameObject _bird;
    protected GameObject _progressGroup;
    protected Slider _progressSld;

    /// <summary>
    /// Auto Binding Section
    /// </summary>
    protected override void OnAutoBind()
    {
        base.OnAutoBind();
        this._bird = this.collector.GetNode("Bird");
        this._progressGroup = this.collector.GetNode("ProgressGroup");
        this._progressSld = this.collector.GetNodeComponent<Slider>("Progress*Sld");
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
        this.BasicDisplay();
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

    protected override void ShowAnimation(AnimationEnd animationEnd)
    {
        animationEnd(); // Must call if animation end
    }

    protected override void HideAnimation(AnimationEnd animationEnd)
    {
        animationEnd(); // Must call if animation end
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

    protected void BasicDisplay()
    {
        this.DrawProgressView(0, 0, 0);
    }

    public void DrawProgressView(float progress, float currentCount, float totalCount)
    {
        this._progressSld.value = progress;
    }
}
