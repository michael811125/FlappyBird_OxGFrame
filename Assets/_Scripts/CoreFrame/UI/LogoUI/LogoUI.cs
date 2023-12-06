﻿using OxGFrame.CoreFrame.UIFrame;
using Cysharp.Threading.Tasks;
using System;

public class LogoUI : UIBase
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
        this._InitComponents();
    }

    protected override void OnShow(object obj)
    {
        /**
         * Do Somethings Init With Every Showing In Here
         */
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
        this._easyAnime.Play("Intro", () => { animeEndCb(); });
    }

    protected override void HideAnime(AnimeEndCb animeEndCb)
    {
        this._easyAnime.Play("Outro", () => { animeEndCb(); });
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

    private EasyAnimation _easyAnime;

    private void _InitComponents()
    {
        this._easyAnime = this.collector.GetNode("EasyAnime").GetComponent<EasyAnimation>();
    }
}