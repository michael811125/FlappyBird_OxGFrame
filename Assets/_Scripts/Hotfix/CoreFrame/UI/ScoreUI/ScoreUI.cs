using OxGFrame.CoreFrame.UIFrame;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace FlappyBird.Hotfix.Runtime
{
    /// <summary>
    /// Score UI. Shows the live score during a run.
    /// <para>
    /// 分數介面。遊戲進行中顯示即時分數。
    /// </para>
    /// </summary>
    public class ScoreUI : UIBase
    {
        #region Binding Components
        protected Text _scoreTxt;

        /// <summary>
        /// Auto binding section
        /// <para>自動綁定區塊</para>
        /// </summary>
        protected override void OnAutoBind()
        {
            base.OnAutoBind();
            this._scoreTxt = this.collector.GetNodeComponent<Text>("Score*Txt");
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
            this._UpdateScoreText();

            if (HCoreSystem.IsGameStart())
            {
                if (Keyboard.current.spaceKey.wasReleasedThisFrame)
                {
                    HCoreSystem.AddScore();
                }
            }
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

        private void _UpdateScoreText()
        {
            this._scoreTxt.text = HCoreSystem.GetScore().ToString();
        }
    }
}