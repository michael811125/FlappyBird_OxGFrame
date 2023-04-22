using System.Collections.Generic;
using UnityEngine;
using OxGFrame.CoreFrame.UIFrame;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using OxGFrame.MediaFrame;

public class SettlementUI : UIBase
{
    public override void BeginInit()
    {
        /**
         * Do Somthing Init Once In Here
         */
    }

    protected override async UniTask OpenSub()
    {
        /**
        * Open Sub With Async
        */
    }

    protected override void CloseSub()
    {
        /**
        * Close Sub
        */
    }

    protected override void InitOnceComponents()
    {
        /**
         * Do Somthing Init Once In Here (For Components)
         */

        this._InitComponents();
    }

    protected override void InitOnceEvents()
    {
        /**
          * Do Somthing Init Once In Here (For Events)
          */

        this._InitEvents();
    }

    protected override void OnShow(object obj)
    {
        /**
         * Do Something Init With Every Showing In Here
         */

        this._DrawScoreView();
        this._DrawMedalView();
    }

    protected override void OnUpdate(float dt)
    {
        /**
         * Do Update Per FrameRate
         */
    }

    protected override void ShowAnim(AnimEndCb animEndCb)
    {
        animEndCb(); // Must Keep, Because Parent Already Set AnimCallback
    }

    protected override void HideAnim(AnimEndCb animEndCb)
    {
        animEndCb(); // Must Keep, Because Parent Already Set AnimCallback
    }

    protected override void OnClose()
    {

    }

    public override void OnRelease()
    {

    }

    // 初始 SettlementUI 相關組件

    public List<Sprite> medals = new List<Sprite>();

    private Text _score;
    private Text _bestScore;
    private Image _medalImg;
    private Button _replayBtn;
    private Button _menuBtn;

    private void _InitComponents()
    {
        this._score = this.collector.GetNode("Score").GetComponent<Text>();
        this._bestScore = this.collector.GetNode("BestScore").GetComponent<Text>();
        this._medalImg = this.collector.GetNode("Medal").GetComponent<Image>();
        this._replayBtn = this.collector.GetNode("Replay").GetComponentInChildren<Button>();
        this._menuBtn = this.collector.GetNode("Menu").GetComponentInChildren<Button>();
    }

    private void _InitEvents()
    {
        this._replayBtn.onClick.RemoveAllListeners();
        this._replayBtn.onClick.AddListener(() =>
        {
            // 播放轉場音效
            MediaFrames.AudioFrame.Play(AudioPath.SwooshingSfx).Forget();

            // 重新遊玩
            CoreManager.Replay();

            // 關閉自身 UI
            this.CloseSelf();
        });

        this._menuBtn.onClick.RemoveAllListeners();
        this._menuBtn.onClick.AddListener(() =>
        {
            // 播放轉場音效
            MediaFrames.AudioFrame.Play(AudioPath.SwooshingSfx).Forget();

            // 前往主選單
            CoreManager.GoToMenu();

            // 關閉自身 UI
            this.CloseSelf();
        });
    }

    private void _DrawScoreView()
    {
        // 顯示當前分數
        this._score.text = CoreManager.GetScore().ToString(); ;
        // 顯示最佳分數
        this._bestScore.text = CoreManager.GetBestScore().ToString();
    }

    private void _DrawMedalView()
    {
        int score = CoreManager.GetScore();

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
