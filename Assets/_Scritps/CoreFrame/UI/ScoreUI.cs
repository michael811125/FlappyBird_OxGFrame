using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OxGFrame.CoreFrame.UIFrame;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ScoreUI : UIBase
{
    public override void OnInit()
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

    protected override void OnBind()
    {
        this._InitComponents();
    }

    protected override void OnShow(object obj)
    {
        /**
         * Do Something Init With Every Showing In Here
         */
    }

    protected override void OnUpdate(float dt)
    {
        /**
         * Do Update Per FrameRate
         */

        this._UpdateScoreText();

        if (CoreManager.IsGameStart())
        {
            if (Keyboard.current.spaceKey.wasReleasedThisFrame)
            {
                CoreManager.AddScore();
            }
        }
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

    // 初始 ScoreUI 相關組件
    private Text _score;

    private void _InitComponents()
    {
        this._score = this.collector.GetNode("Score").GetComponent<Text>();
    }

    private void _UpdateScoreText()
    {
        this._score.text = CoreManager.GetScore().ToString();
    }
}
