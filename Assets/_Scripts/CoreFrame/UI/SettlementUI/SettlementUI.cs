using System.Collections.Generic;
using UnityEngine;
using OxGFrame.CoreFrame.UIFrame;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using OxGFrame.MediaFrame;
using OxGKit.Utilities.Button;

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
    /// Auto Binding Section
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
        this._InitEvents();
    }

    protected override void OnShow(object obj)
    {
        this._DrawScoreView();
        this._DrawMedalView();
    }

    protected override void OnUpdate(float dt)
    {
        /**
         * Do Update Per FrameRate
         */
    }

    protected override void ShowAnime(AnimeEndCb animeEndCb)
    {
        animeEndCb(); // Must Keep, Because Parent Already Set AnimCallback
    }

    protected override void HideAnime(AnimeEndCb animeEndCb)
    {
        animeEndCb(); // Must Keep, Because Parent Already Set AnimCallback
    }

    protected override void OnClose()
    {

    }

    public override void OnRelease()
    {

    }

    public List<Sprite> medals = new List<Sprite>();

    private void _InitEvents()
    {
        this._replayBtnPlus.onClick.RemoveAllListeners();
        this._replayBtnPlus.onClick.AddListener(() =>
        {
            // 播放轉場音效
            MediaFrames.AudioFrame.Play(Audios.SwooshingSfx).Forget();

            // 重新遊玩
            CoreSystem.Replay();

            // 關閉自身 UI
            this.CloseSelf();
        });

        this._menuBtnPlus.onClick.RemoveAllListeners();
        this._menuBtnPlus.onClick.AddListener(() =>
        {
            // 播放轉場音效
            MediaFrames.AudioFrame.Play(Audios.SwooshingSfx).Forget();

            // 前往主選單
            CoreSystem.GoToMenu();

            // 關閉自身 UI
            this.CloseSelf();
        });
    }

    private void _DrawScoreView()
    {
        // 顯示當前分數
        this._scoreTxt.text = CoreSystem.GetScore().ToString();
        // 顯示最佳分數
        this._bestScoreTxt.text = CoreSystem.GetBestScore().ToString();
    }

    private void _DrawMedalView()
    {
        int score = CoreSystem.GetScore();

        // 分數 >= 40 分 (白金牌)
        if (score >= 40)
        {
            this._medalImg.sprite = this.medals[3];
        }
        // 分數 >= 30 分 (金牌)
        else if (score >= 30)
        {
            this._medalImg.sprite = this.medals[2];
        }
        // 分數 >= 20 分 (銀牌)
        else if (score >= 20)
        {
            this._medalImg.sprite = this.medals[1];
        }
        // 分數 >= 10 分 (銅牌)
        else if (score >= 10)
        {
            this._medalImg.sprite = this.medals[0];
        }
        // 沒到達分數, 關閉獎牌顯示
        else this._medalImg.gameObject.SetActive(false);
    }
}
