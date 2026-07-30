using OxGFrame.CoreFrame.UIFrame;
using Cysharp.Threading.Tasks;

namespace FlappyBird.Hotfix.Runtime
{
    /// <summary>
    /// Get ready UI. Uses MaskEvent so that tapping anywhere starts the run and closes
    /// this UI.
    /// <para>
    /// 準備介面。利用 MaskEvent, 讓玩家點擊任意處即開始遊戲並關閉此介面。
    /// </para>
    /// </summary>
    public class GetReadyUI : UIBase
    {
        /// <summary>
        /// Called when the mask is clicked.
        /// <para>點擊遮罩時呼叫。</para>
        /// </summary>
        protected override void MaskEvent()
        {
            HCoreSystem.StartGame();
            this.CloseSelf();
        }

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
    }
}