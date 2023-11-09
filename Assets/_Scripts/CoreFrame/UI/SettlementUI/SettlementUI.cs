﻿using System.Collections.Generic;
using UnityEngine;
using OxGFrame.CoreFrame.UIFrame;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using OxGFrame.MediaFrame;

public class SettlementUI : UIBase
{
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
        this._InitComponents();
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

    // 初始 SettlementUI 相關組件

    public List<Sprite> medals = new List<Sprite>();

    private Text _score;
    private Text _bestScore;
    private Image _medalImg;
    private Button _replayBtn;
    private Button _menuBtn;

    private void _InitComponents()
    {
        this._score = this.collector.GetNodeComponent<Text>("Score");
        this._bestScore = this.collector.GetNodeComponent<Text>("BestScore");
        this._medalImg = this.collector.GetNodeComponent<Image>("Medal");
        this._replayBtn = this.collector.GetNode("Replay").GetComponentInChildren<Button>();
        this._menuBtn = this.collector.GetNode("Menu").GetComponentInChildren<Button>();
    }

    private void _InitEvents()
    {
        this._replayBtn.onClick.RemoveAllListeners();
        this._replayBtn.onClick.AddListener(() =>
        {
            // 播放轉場音效
            MediaFrames.AudioFrame.Play(Audios.SwooshingSfx).Forget();

            // 重新遊玩
            CoreSystem.Replay();

            // 關閉自身 UI
            this.CloseSelf();
        });

        this._menuBtn.onClick.RemoveAllListeners();
        this._menuBtn.onClick.AddListener(() =>
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
        this._score.text = CoreSystem.GetScore().ToString();
        // 顯示最佳分數
        this._bestScore.text = CoreSystem.GetBestScore().ToString();
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
