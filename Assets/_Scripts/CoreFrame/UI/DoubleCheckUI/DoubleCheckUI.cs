using OxGFrame.CoreFrame.UIFrame;
using Cysharp.Threading.Tasks;
using OxGKit.Utilities.Button;
using TMPro;
using System;

public class DoubleCheckUI : UIBase
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
         * On Pre-Show With Async
         */
    }

    protected override void OnPreClose()
    {
        /**
         * On Pre-Close
         */
    }

    protected override void OnBind()
    {
        this.InitComponents();
    }

    protected override void OnShow(object obj)
    {
        this.BasicDisplay(obj);
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

    protected Action _noAction;
    protected Action _yesAction;

    protected TMP_Text _titleTmpTxt;
    protected TMP_Text _msgTmpTxt;
    protected ButtonPlus _noBtnPlus;
    protected TMP_Text _noBtnTmpTxt;
    protected ButtonPlus _yesBtnPlus;
    protected TMP_Text _yesBtnTmpTxt;

    protected void InitComponents()
    {
        this._titleTmpTxt = this.collector.GetNodeComponent<TMP_Text>("Title*TmpTxt");
        this._msgTmpTxt = this.collector.GetNodeComponent<TMP_Text>("Msg*TmpTxt");
        this._noBtnPlus = this.collector.GetNodeComponent<ButtonPlus>("No*BtnPlus");
        this._noBtnTmpTxt = this.collector.GetNodeComponent<TMP_Text>("NoBtn*TmpTxt");
        this._yesBtnPlus = this.collector.GetNodeComponent<ButtonPlus>("Yes*BtnPlus");
        this._yesBtnTmpTxt = this.collector.GetNodeComponent<TMP_Text>("YesBtn*TmpTxt");
    }

    protected void BasicDisplay(object obj)
    {
        object[] args = obj as object[];

        if (args == null) return;

        string title = args?[0].ToString();
        string msg = args?[1].ToString();
        this._yesAction = args?[2] as Action;
        this._noAction = args?[3] as Action;

        this.DrawTitleView(title);
        this.DrawMsgView(msg);
        this.DrawButtonsView();
    }

    protected void DrawTitleView(string title)
    {
        this._titleTmpTxt.text = title;
    }

    protected void DrawMsgView(string msg)
    {
        this._msgTmpTxt.text = msg;
    }

    protected void DrawButtonsView()
    {
        this._yesBtnPlus.On(() =>
        {
            this._yesAction?.Invoke();
            this._yesAction = null;
            this.CloseSelf();
        }, null);

        this._noBtnPlus.On(() =>
        {
            this._noAction?.Invoke();
            this._noAction = null;
            this.CloseSelf();
        }, null);
    }
}