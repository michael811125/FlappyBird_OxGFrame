using OxGFrame.CoreFrame.UIFrame;
using Cysharp.Threading.Tasks;

namespace FlappyBird.Hotfix.Runtime
{
    public class GetReadyUI : UIBase
    {
        protected override void MaskEvent()
        {
            HCoreSystem.StartGame();
            this.CloseSelf();
        }

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
            /**
             * Do Update Per FrameRate
             */
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
    }
}