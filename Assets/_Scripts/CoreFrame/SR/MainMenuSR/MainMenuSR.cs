﻿using UnityEngine;
using Cysharp.Threading.Tasks;
using OxGFrame.CoreFrame.SRFrame;

public class MainMenuSR : SRBase
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

    }

    protected override void OnShow(object obj)
    {
        /**
         * Do Somethings Init With Every Showing In Here
         */
    }

    protected override void OnUpdate(float dt)
    {
        this._UpdateGroundScroll();
    }

    protected override void OnClose()
    {

    }

    public override void OnRelease()
    {

    }

    // 初始 MenuBackgroundSC 相關組件

    public float scrollSpeed = 0.5f;
    // 拖曳方式 (drag assign it)
    public Renderer ground;

    private void _UpdateGroundScroll()
    {
        Vector2 textureOffset = new Vector2(Time.time * this.scrollSpeed, 0);
        this.ground.material.mainTextureOffset = textureOffset;
    }
}
