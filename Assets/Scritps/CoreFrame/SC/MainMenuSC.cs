using UnityEngine;
using Cysharp.Threading.Tasks;
using OxGFrame.CoreFrame.GSFrame;

public class MainMenuSC : GSBase
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
    }

    protected override void InitOnceEvents()
    {
        /**
          * Do Somthing Init Once In Here (For Events)
          */
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
