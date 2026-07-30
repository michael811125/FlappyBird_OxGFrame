using System.Collections.Generic;
using UnityEngine;
using OxGFrame.CoreFrame.UIFrame;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using OxGFrame.MediaFrame;
using OxGKit.ButtonSystem;

/// <summary>
/// Settlement UI. Shows the final and best score, picks a medal by score threshold,
/// and offers replay / back to menu.
/// <para>
/// 結算介面。顯示本局與最佳分數, 依分數門檻決定獎牌,
/// 並提供重新遊玩 / 返回主選單。
/// </para>
/// </summary>
public class SettlementUI : UIBase
{
    #region Binding Components
    protected Text _scoreTxt;
    protected Text _bestScoreTxt;
    protected Image _medalImg;
    protected GameObject _replay;
    protected ButtonPlus _replayBtnPlus;
    protected GameObject _menu;
    protected ButtonPlus _menuBtnPlus;

    /// <summary>
    /// Auto binding section
    /// <para>自動綁定區塊</para>
    /// </summary>
    protected override void OnAutoBind()
    {
        base.OnAutoBind();
        this._scoreTxt = this.collector.GetNodeComponent<Text>("Score*Txt");
        this._bestScoreTxt = this.collector.GetNodeComponent<Text>("BestScore*Txt");
        this._medalImg = this.collector.GetNodeComponent<Image>("Medal*Img");
        this._replay = this.collector.GetNode("Replay");
        this._replayBtnPlus = this.collector.GetNodeComponent<ButtonPlus>("Replay*BtnPlus");
        this._menu = this.collector.GetNode("Menu");
        this._menuBtnPlus = this.collector.GetNodeComponent<ButtonPlus>("Menu*BtnPlus");
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
        this._InitEvents();
    }

    /// <summary>
    /// Called on every show.
    /// <para>每次顯示時呼叫。</para>
    /// </summary>
    protected override void OnShow(object obj)
    {
        this._DrawScoreView();
        this._DrawMedalView();
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

    }

    /// <summary>
    /// Called on release (CloseAndDestroy).
    /// <para>釋放時呼叫 (CloseAndDestroy)。</para>
    /// </summary>
    public override void OnRelease()
    {

    }

    public List<Sprite> medals = new List<Sprite>();

    private void _InitEvents()
    {
        this._replayBtnPlus.onClick.RemoveAllListeners();
        this._replayBtnPlus.onClick.AddListener(() =>
        {
            // Play the transition sound effect
            // 播放轉場音效
            MediaFrames.AudioFrame.Play(Pkgs.PatchPkg, Audios.SwooshingSfx).Forget();

            // Replay
            // 重新遊玩
            CoreSystem.Replay();

            // Close this UI itself
            // 關閉自身 UI
            this.CloseSelf();
        });

        this._menuBtnPlus.onClick.RemoveAllListeners();
        this._menuBtnPlus.onClick.AddListener(() =>
        {
            // Play the transition sound effect
            // 播放轉場音效
            MediaFrames.AudioFrame.Play(Pkgs.PatchPkg, Audios.SwooshingSfx).Forget();

            // Go to the main menu
            // 前往主選單
            CoreSystem.GoToMenu();

            // Close this UI itself
            // 關閉自身 UI
            this.CloseSelf();
        });
    }

    private void _DrawScoreView()
    {
        // Show the current score
        // 顯示當前分數
        this._scoreTxt.text = CoreSystem.GetScore().ToString();
        // Show the best score
        // 顯示最佳分數
        this._bestScoreTxt.text = CoreSystem.GetBestScore().ToString();
    }

    private void _DrawMedalView()
    {
        int score = CoreSystem.GetScore();

        // Score >= 40 (platinum medal)
        // 分數 >= 40 分 (白金牌)
        if (score >= 40)
        {
            this._medalImg.sprite = this.medals[3];
        }
        // Score >= 30 (gold medal)
        // 分數 >= 30 分 (金牌)
        else if (score >= 30)
        {
            this._medalImg.sprite = this.medals[2];
        }
        // Score >= 20 (silver medal)
        // 分數 >= 20 分 (銀牌)
        else if (score >= 20)
        {
            this._medalImg.sprite = this.medals[1];
        }
        // Score >= 10 (bronze medal)
        // 分數 >= 10 分 (銅牌)
        else if (score >= 10)
        {
            this._medalImg.sprite = this.medals[0];
        }
        // Below every threshold, hide the medal
        // 沒到達分數, 關閉獎牌顯示
        else this._medalImg.gameObject.SetActive(false);
    }
}
