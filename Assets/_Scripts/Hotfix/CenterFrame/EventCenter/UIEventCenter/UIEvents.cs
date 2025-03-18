using Cysharp.Threading.Tasks;
using OxGFrame.CenterFrame.EventCenter;
using OxGFrame.CoreFrame;
using OxGKit.LoggingSystem;
using System;

namespace FlappyBird.Hotfix.Runtime
{
    /*
     * Rule
     * UI: E + assetName => Example: BackpackUI => EBackpackUI
     * Other: E + Name => Example: AddCoin => EAddCoin
     */


    /// <summary>
    /// Show DescriptionUI Event (Common)
    /// </summary>
    public class EDescriptionUI : EventBase
    {
        private string _msg;
        private Action _closeAction;

        public void Emit(string msg, Action closeAction = null)
        {
            this._msg = msg;
            this._closeAction = closeAction;

            this.HandleEvent().Forget();
        }

        public async override UniTaskVoid HandleEvent()
        {
            Logging.Print<HLogger>($"<color=#FFC078>【Handle Event】 -> {nameof(EDescriptionUI)}</color>");

            await CoreFrames.UIFrame.Show<DescriptionUI>(Pkgs.PatchPkg, UIs.DescriptionUI, new object[] { this._msg, this._closeAction }, UIs.AwaitingUI, 0);

            this.Release();
        }

        protected override void Release()
        {
            this._msg = null;
            this._closeAction = null;
        }
    }

    /// <summary>
    /// Show DoubleCheckUI Event (Common)
    /// </summary>
    public class EDoubleCheckUI : EventBase
    {
        private string _title;
        private string _msg;
        private Action _yesAction;
        private Action _noAction;

        public void Emit(string title, string msg, Action yesAction = null, Action noAction = null)
        {
            this._title = title;
            this._msg = msg;
            this._yesAction = yesAction;
            this._noAction = noAction;

            this.HandleEvent().Forget();
        }

        public async override UniTaskVoid HandleEvent()
        {
            Logging.Print<HLogger>($"<color=#FFC078>【Handle Event】 -> {nameof(EDoubleCheckUI)}</color>");

            await CoreFrames.UIFrame.Show<DoubleCheckUI>(Pkgs.PatchPkg, UIs.DoubleCheckUI, new object[] { this._title, this._msg, this._yesAction, this._noAction }, UIs.AwaitingUI, 0);

            this.Release();
        }

        protected override void Release()
        {
            this._msg = null;
            this._yesAction = null;
            this._noAction = null;
        }
    }
}