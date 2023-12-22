using OxGFrame.CoreFrame.UIFrame;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

public class DescriptionUI : UIBase
{
    // Use _Node@XXX to Bind

    #region Binding Components
    protected GameObject _layout;
    protected TMP_Text _descTmpTxt;

    /// <summary>
    /// Auto Binding Section
    /// </summary>
    protected override void OnAutoBind()
    {
        base.OnAutoBind();
        this._layout = this.collector.GetNode("Layout");
        this._descTmpTxt = this.collector.GetNodeComponent<TMP_Text>("Desc*TmpTxt");
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

    protected override void MaskEvent()
    {
        this._closeAction?.Invoke();
        this._closeAction = null;
        this.CloseSelf();
    }

    // Data
    private Action _closeAction;

    protected void BasicDisplay(object obj)
    {
        object[] args = obj as object[];

        if (args == null) return;

        string msg = args?[0].ToString();
        this._closeAction = args?[1] as Action;

        this._DrawDescTextView(msg);
    }

    private void _DrawDescTextView(string msg)
    {
        this._descTmpTxt.text = msg;

        // After set text must rebuild layout, because background image must be adapter with text size
        LayoutRebuilder.ForceRebuildLayoutImmediate(this._layout.transform as RectTransform);
    }
}
