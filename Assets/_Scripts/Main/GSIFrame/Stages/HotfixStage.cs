using Cysharp.Threading.Tasks;
using OxGFrame.CoreFrame;
using OxGFrame.GSIFrame;
using OxGFrame.Hotfixer;
using OxGFrame.Hotfixer.HotfixEvent;
using OxGFrame.Hotfixer.HotfixFsm;
using OxGKit.LoggingSystem;
using OxGKit.TimeSystem;
using UniFramework.Event;

namespace FlappyBird.Main.Runtime
{
    public class HotfixStage : GSIBase
    {
        private enum HotfixStep
        {
            NONE,
            INIT_TIMER,
            START_CHECK_HOTFIX,
            WAITING_FOR_HOTFIX,
            WAITING_FOR_BUFFER_TIME,
            LOAD_HOTFIX_MAIN_SCENE,
            DONE
        }

        public const string HotfixPkg = "HotfixPkg";
        public const string HMain = "HMain";

        private HotfixStep _step = HotfixStep.NONE;
        private RealTimer _realTimer;

        public async override UniTask OnCreate()
        {
            // Init Hotfix Events
            this._InitHotfixEvents();

            this._realTimer = new RealTimer();
        }

        public async override UniTask OnEnter()
        {
            this._step = HotfixStep.INIT_TIMER;
        }

        public override void OnUpdate(float dt = 0.0f)
        {
            switch (this._step)
            {
                case HotfixStep.INIT_TIMER:
                    // Set buffer timer (doing background)
                    this._realTimer.Reset();
                    this._realTimer.Play();
                    this._realTimer.SetTimer(3f);

                    // Change next step immediately
                    this._step = HotfixStep.START_CHECK_HOTFIX;
                    break;

                // Start check hotfix while logo showing (doing background)
                case HotfixStep.START_CHECK_HOTFIX:
                    // Do hotfix check, also can read hotfixconfig.conf from StreamingAssets
                    Hotfixers.CheckHotfix
                    (
                        HotfixPkg,
                        new string[]
                        {
                            "UniTask.dll",
                            "mscorlib.dll",
                            "System.Core.dll",
                            "Newtonsoft.Json.dll",
                            "OxGKit.Utilities.Runtime.dll",
                            "OxGKit.SingletonSystem.Runtime.dll",
                            "OxGFrame.CenterFrame.Runtime.dll"
                        },
                        new string[]
                        {
                            "FlappyBird.Hotfix.Runtime.dll"
                        }
                    );

                    // Change next step immediately
                    this._step = HotfixStep.WAITING_FOR_HOTFIX;
                    break;

                // Waiting for hotfix are all done
                case HotfixStep.WAITING_FOR_HOTFIX:
                    // Check hotfix are all done per frame rate
                    if (Hotfixers.IsDone())
                    {
                        this._step = HotfixStep.WAITING_FOR_BUFFER_TIME;
                        Logging.Print<MLogger>("<color=#7bff9e>Hotfix finished.</color>");
                    }
                    break;

                case HotfixStep.WAITING_FOR_BUFFER_TIME:
                    // If buffer timeout
                    if (this._realTimer.IsTimerTimeout())
                    {
                        // Set timer for outro anim
                        this._realTimer.SetTimer(1f);

                        // Close LogoUI
                        CoreFrames.UIFrame.Close(LogoStage.LogoUI);

                        // If after hotfix loaded and buffer timeout, will change next step
                        this._step = HotfixStep.LOAD_HOTFIX_MAIN_SCENE;
                    }
                    break;

                // Load hotfix main scene
                case HotfixStep.LOAD_HOTFIX_MAIN_SCENE:
                    // If outro buffer timeout
                    if (this._realTimer.IsTimerTimeout())
                    {
                        this._LoadHotfixMainScene().Forget();
                    }
                    break;

                // Nothing to do
                case HotfixStep.DONE:
                    break;
            }
        }

        public override void OnExit()
        {
        }

        private async UniTask _LoadHotfixMainScene()
        {
            this.StopUpdate();

            // Start load single scene from HotfixPackage
            await CoreFrames.USFrame.LoadSingleSceneAsync(HotfixPkg, HMain);

            // Hotfix done
            this._step = HotfixStep.DONE;
        }

        #region Hotfix Event
        private EventGroup _hotfixEvents = new EventGroup();
        private void _InitHotfixEvents()
        {
            // 0. HotfixFsmState
            // 1. HotfixInitFailed
            // 2. HotfixUpdateFailed
            // 3. HotfixDownloadFailed

            #region Add HotfixEvents Handle
            this._hotfixEvents.AddListener<HotfixEvents.HotfixFsmState>(this._OnHandleEventMessage);
            this._hotfixEvents.AddListener<HotfixEvents.HotfixInitFailed>(this._OnHandleEventMessage);
            this._hotfixEvents.AddListener<HotfixEvents.HotfixUpdateFailed>(this._OnHandleEventMessage);
            this._hotfixEvents.AddListener<HotfixEvents.HotfixDownloadFailed>(this._OnHandleEventMessage);
            #endregion
        }

        private void _OnHandleEventMessage(IEventMessage message)
        {
            if (message is HotfixEvents.HotfixFsmState)
            {
                HotfixEvents.HotfixFsmState msgData = message as HotfixEvents.HotfixFsmState;

                switch (msgData.stateNode)
                {
                    case HotfixFsmStates.FsmHotfixPrepare:
                        break;
                    case HotfixFsmStates.FsmInitHotfixPackage:
                        break;
                    case HotfixFsmStates.FsmUpdateHotfixPackage:
                        break;
                    case HotfixFsmStates.FsmHotfixCreateDownloader:
                        break;
                    case HotfixFsmStates.FsmHotfixBeginDownload:
                        break;
                    case HotfixFsmStates.FsmHotfixDownloadOver:
                        break;
                    case HotfixFsmStates.FsmHotfixClearCache:
                        break;
                    case HotfixFsmStates.FsmLoadAOTAssemblies:
                        break;
                    case HotfixFsmStates.FsmLoadHotfixAssemblies:
                        break;
                    case HotfixFsmStates.FsmHotfixDone:
                        break;
                }
            }
            else if (message is HotfixEvents.HotfixInitFailed)
            {
                HotfixUserEvents.UserTryInitHotfix.SendEventMessage();
            }
            else if (message is HotfixEvents.HotfixUpdateFailed)
            {
                HotfixUserEvents.UserTryUpdateHotfix.SendEventMessage();
            }
            else if (message is HotfixEvents.HotfixDownloadFailed)
            {
                HotfixUserEvents.UserTryCreateDownloader.SendEventMessage();
            }
            else
            {
                throw new System.NotImplementedException($"{message.GetType()}");
            }
        }
        #endregion
    }
}