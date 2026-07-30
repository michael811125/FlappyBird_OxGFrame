using UnityEngine;
using Cysharp.Threading.Tasks;
using OxGFrame.CoreFrame.SRFrame;

namespace FlappyBird.Hotfix.Runtime
{
    /// <summary>
    /// Main menu scene resource. Only scrolls the ground texture; everything
    /// interactive lives in MainMenuUI.
    /// <para>
    /// 主選單的場景資源。僅負責捲動地面貼圖, 互動部分都在 MainMenuUI。
    /// </para>
    /// </summary>
    public class MainMenuSR : SRBase
    {
        #region Binding Components
        protected Renderer _groundRen;

        /// <summary>
        /// Auto binding section
        /// <para>自動綁定區塊</para>
        /// </summary>
        protected override void OnAutoBind()
        {
            base.OnAutoBind();
            this._groundRen = this.collector.GetNodeComponent<Renderer>("Ground*Ren");
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
            /**
             * Do somethings init with every showing in here
             * 在此處進行每次顯示都會執行的初始化
             */
        }

        /// <summary>
        /// Called per frame while showing.
        /// <para>顯示期間每幀呼叫。</para>
        /// </summary>
        protected override void OnUpdate(float dt)
        {
            this._UpdateGroundScroll();
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

        public float scrollSpeed = 0.5f;

        private void _UpdateGroundScroll()
        {
            Vector2 textureOffset = new Vector2(Time.time * this.scrollSpeed, 0);
            this._groundRen.material.mainTextureOffset = textureOffset;
        }
    }
}