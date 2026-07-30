using Cysharp.Threading.Tasks;
using OxGFrame.CoreFrame.UIFrame;
using OxGKit.Utilities.EasyAnim;

namespace FlappyBird.Main.Runtime
{
    /// <summary>
    /// Logo UI. Plays its Intro / Outro clips through EasyAnimation inside
    /// OnShowAnimation / OnCloseAnimation.
    /// <para>
    /// 商標介面。於 OnShowAnimation / OnCloseAnimation 中
    /// 透過 EasyAnimation 播放 Intro / Outro 動畫。
    /// </para>
    /// </summary>
    public class LogoUI : UIBase
    {
        // Use _Node@XXX on the prefab to bind nodes
        // 於 Prefab 上使用 _Node@XXX 命名進行節點綁定

        #region Binding Components
        protected EasyAnimation _transitionEasyAnim;

        /// <summary>
        /// Auto binding section
        /// <para>自動綁定區塊</para>
        /// </summary>
        protected override void OnAutoBind()
        {
            base.OnAutoBind();
            this._transitionEasyAnim = this.collector.GetNodeComponent<EasyAnimation>("Transition*EasyAnim");
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
            /**
             * Do update per frame rate
             * 依幀率進行更新
             */
        }

        /// <summary>
        /// Called once after data is received, refresh the view here.
        /// <para>收到資料後呼叫一次, 在此刷新畫面。</para>
        /// </summary>
        public override void OnReceiveAndRefresh(object obj = null)
        {
            /**
             * Do refresh once after data receive
             * 收到資料後刷新一次
             */
        }

        /// <summary>
        /// Show animation, animationEnd must be invoked when it finishes.
        /// <para>顯示動畫, 結束時必須呼叫 animationEnd。</para>
        /// </summary>
        protected override void OnShowAnimation(AnimationEnd animationEnd)
        {
            this._transitionEasyAnim.Play("Intro", () => { animationEnd(); });
        }

        /// <summary>
        /// Close animation, animationEnd must be invoked when it finishes.
        /// <para>關閉動畫, 結束時必須呼叫 animationEnd。</para>
        /// </summary>
        protected override void OnCloseAnimation(AnimationEnd animationEnd)
        {
            this._transitionEasyAnim.Play("Outro", () => { animationEnd(); });
        }

        /// <summary>
        /// Called on close.
        /// <para>關閉時呼叫。</para>
        /// </summary>
        protected override void OnClose()
        {
            /**
             * Do somethings on close (Close)
             * 關閉時執行 (Close)
             */
        }

        /// <summary>
        /// Called on release (CloseAndDestroy).
        /// <para>釋放時呼叫 (CloseAndDestroy)。</para>
        /// </summary>
        public override void OnRelease()
        {
            /**
             * Do somethings on release (CloseAndDestroy)
             * 釋放時執行 (CloseAndDestroy)
             */
        }
    }
}