using OxGFrame.CoreFrame.UIFrame;
using Cysharp.Threading.Tasks;
using OxGKit.ButtonSystem;
using TMPro;
using System;

/// <summary>
/// Double check UI (common). Shows a title, a message and yes / no callbacks; it is
/// opened through the EDoubleCheckUI event.
/// <para>
/// 重複確認介面 (通用)。顯示標題、訊息與 yes / no 回調,
/// 透過 EDoubleCheckUI 事件開啟。
/// </para>
/// </summary>
public class DoubleCheckUI : UIBase
{
    // Use _Node@XXX on the prefab to bind nodes
    // 於 Prefab 上使用 _Node@XXX 命名進行節點綁定

    #region Binding Components
    protected TMP_Text _titleTmpTxt;
    protected TMP_Text _msgTmpTxt;
    protected ButtonPlus _noBtnPlus;
    protected TMP_Text _noBtnTmpTxt;
    protected ButtonPlus _yesBtnPlus;
    protected TMP_Text _yesBtnTmpTxt;

    /// <summary>
    /// Auto binding section
    /// <para>自動綁定區塊</para>
    /// </summary>
    protected override void OnAutoBind()
    {
        base.OnAutoBind();
        this._titleTmpTxt = this.collector.GetNodeComponent<TMP_Text>("Title*TmpTxt");
        this._msgTmpTxt = this.collector.GetNodeComponent<TMP_Text>("Msg*TmpTxt");
        this._noBtnPlus = this.collector.GetNodeComponent<ButtonPlus>("No*BtnPlus");
        this._noBtnTmpTxt = this.collector.GetNodeComponent<TMP_Text>("NoBtn*TmpTxt");
        this._yesBtnPlus = this.collector.GetNodeComponent<ButtonPlus>("Yes*BtnPlus");
        this._yesBtnTmpTxt = this.collector.GetNodeComponent<TMP_Text>("YesBtn*TmpTxt");
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
         * On pre-show with async
         * 以非同步進行顯示前處理
         */
    }

    /// <summary>
    /// Called before closing, close sub objects here.
    /// <para>關閉前呼叫, 可在此關閉子物件。</para>
    /// </summary>
    protected override void OnPreClose()
    {
        /**
         * On pre-close
         * 進行關閉前處理
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

    protected Action _noAction;
    protected Action _yesAction;

    private void _BasicDisplay(object obj)
    {
        object[] args = obj as object[];

        if (args == null) return;

        string title = args?[0].ToString();
        string msg = args?[1].ToString();
        this._yesAction = args?[2] as Action;
        this._noAction = args?[3] as Action;

        this._DrawTitleView(title);
        this._DrawMsgView(msg);
        this._DrawButtonsView();
    }

    protected void _DrawTitleView(string title)
    {
        this._titleTmpTxt.text = title;
    }

    protected void _DrawMsgView(string msg)
    {
        this._msgTmpTxt.text = msg;
    }

    protected void _DrawButtonsView()
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
