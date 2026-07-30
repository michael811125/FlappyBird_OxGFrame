using OxGFrame.CoreFrame.UIFrame;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

/// <summary>
/// Description UI (common). Shows a message plus a close callback; it is opened
/// through the EDescriptionUI event rather than being called directly.
/// <para>
/// 說明介面 (通用)。顯示訊息與關閉回調,
/// 透過 EDescriptionUI 事件開啟, 而非直接呼叫。
/// </para>
/// </summary>
public class DescriptionUI : UIBase
{
    // Use _Node@XXX on the prefab to bind nodes
    // 於 Prefab 上使用 _Node@XXX 命名進行節點綁定

    #region Binding Components
    protected GameObject _layout;
    protected TMP_Text _descTmpTxt;

    /// <summary>
    /// Auto binding section
    /// <para>自動綁定區塊</para>
    /// </summary>
    protected override void OnAutoBind()
    {
        base.OnAutoBind();
        this._layout = this.collector.GetNode("Layout");
        this._descTmpTxt = this.collector.GetNodeComponent<TMP_Text>("Desc*TmpTxt");
    }
    #endregion

    /// <summary>
    /// Called once when the instance is created.
    /// <para>實例建立時呼叫一次。</para>
    /// </summary>
    public override void OnCreate()
    {
        /**
         * Do somethings init once in here
         * 在此處進行僅一次的初始化
         */
    }

    /// <summary>
    /// Called before showing, open sub objects here with async.
    /// <para>顯示前呼叫, 可在此以非同步開啟子物件。</para>
    /// </summary>
    protected override async UniTask OnPreShow()
    {
        /**
         * Open sub with async
         * 以非同步開啟子物件
         */
    }

    /// <summary>
    /// Called before closing, close sub objects here.
    /// <para>關閉前呼叫, 可在此關閉子物件。</para>
    /// </summary>
    protected override void OnPreClose()
    {
        /**
         * Close sub
         * 關閉子物件
         */
    }

    /// <summary>
    /// Bind component events here.
    /// <para>在此處綁定元件事件。</para>
    /// </summary>
    protected override void OnBind()
    {
    }

    /// <summary>
    /// Called on every show.
    /// <para>每次顯示時呼叫。</para>
    /// </summary>
    protected override void OnShow(object obj)
    {
        this._BasicDisplay(obj);
    }

    /// <summary>
    /// Called per frame while showing.
    /// <para>顯示期間每幀呼叫。</para>
    /// </summary>
    protected override void OnUpdate(float dt)
    {
        /**
         * Do update per frame rate
         * 依幀率進行更新
         */
    }

    /// <summary>
    /// Called once after data is received, refresh the view here.
    /// <para>收到資料後呼叫一次, 在此刷新畫面。</para>
    /// </summary>
    public override void OnReceiveAndRefresh(object obj = null)
    {
        /**
         * Do refresh once after data receive
         * 收到資料後刷新一次
         */
    }

    /// <summary>
    /// Show animation, animationEnd must be invoked when it finishes.
    /// <para>顯示動畫, 結束時必須呼叫 animationEnd。</para>
    /// </summary>
    protected override void OnShowAnimation(AnimationEnd animationEnd)
    {
        // Must be called when the animation ends
        // 動畫結束時必須呼叫
        animationEnd();
    }

    /// <summary>
    /// Close animation, animationEnd must be invoked when it finishes.
    /// <para>關閉動畫, 結束時必須呼叫 animationEnd。</para>
    /// </summary>
    protected override void OnCloseAnimation(AnimationEnd animationEnd)
    {
        // Must be called when the animation ends
        // 動畫結束時必須呼叫
        animationEnd();
    }

    /// <summary>
    /// Called on close.
    /// <para>關閉時呼叫。</para>
    /// </summary>
    protected override void OnClose()
    {
        /**
         * Do somethings on close (Close)
         * 關閉時執行 (Close)
         */
    }

    /// <summary>
    /// Called on release (CloseAndDestroy).
    /// <para>釋放時呼叫 (CloseAndDestroy)。</para>
    /// </summary>
    public override void OnRelease()
    {
        /**
         * Do somethings on release (CloseAndDestroy)
         * 釋放時執行 (CloseAndDestroy)
         */
    }

    /// <summary>
    /// Called when the mask is clicked.
    /// <para>點擊遮罩時呼叫。</para>
    /// </summary>
    protected override void MaskEvent()
    {
        this._closeAction?.Invoke();
        this._closeAction = null;
        this.CloseSelf();
    }

    // Data
    // 資料
    private Action _closeAction;

    private void _BasicDisplay(object obj)
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

        // The layout must be rebuilt after setting the text, so the background image fits the text size
        // 設置文字後必須重建 Layout, 因為背景圖需要隨文字大小自適應
        LayoutRebuilder.ForceRebuildLayoutImmediate(this._layout.transform as RectTransform);
    }
}
