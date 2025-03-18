using OxGFrame.CoreFrame.UIFrame;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace FlappyBird.Hotfix.Runtime
{
    public class ScoreUI : UIBase
    {
        #region Binding Components
        protected Text _scoreTxt;

        /// <summary>
        /// Auto Binding Section
        /// </summary>
        protected override void OnAutoBind()
        {
            base.OnAutoBind();
            this._scoreTxt = this.collector.GetNodeComponent<Text>("Score*Txt");
        }
        #endregion

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
            this._UpdateScoreText();

            if (HCoreSystem.IsGameStart())
            {
                if (Keyboard.current.spaceKey.wasReleasedThisFrame)
                {
                    HCoreSystem.AddScore();
                }
            }
        }

        protected override void OnShowAnimation(AnimationEnd animationEnd)
        {
            animationEnd(); // Must call if animation end
        }

        protected override void OnCloseAnimation(AnimationEnd animationEnd)
        {
            animationEnd(); // Must call if animation end
        }

        protected override void OnClose()
        {

        }

        public override void OnRelease()
        {

        }

        private void _UpdateScoreText()
        {
            this._scoreTxt.text = HCoreSystem.GetScore().ToString();
        }
    }
}