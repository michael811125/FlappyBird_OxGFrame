using OxGFrame.CoreFrame.UIFrame;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : UIBase
{
    // Use _Node@XXX to Bind

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
        this.InitComponents();
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

    protected override void ShowAnime(AnimeEndCb animeEndCb)
    {
        animeEndCb(); // Must Keep, Because Parent Already Set AnimeCallback
    }

    protected override void HideAnime(AnimeEndCb animeEndCb)
    {
        animeEndCb(); // Must Keep, Because Parent Already Set AnimeCallback
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

    protected GameObject _progressGroup;
    protected Slider _progressSld;

    protected void InitComponents()
    {
        this._progressGroup = this.collector.GetNode("ProgressGroup");
        this._progressSld = this.collector.GetNodeComponent<Slider>("Progress*Sld");
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
