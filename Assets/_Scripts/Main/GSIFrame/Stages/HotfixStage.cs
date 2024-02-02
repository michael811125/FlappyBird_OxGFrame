using Cysharp.Threading.Tasks;
using OxGFrame.CoreFrame;
using OxGFrame.GSIFrame;
using OxGFrame.Hotfixer;
using OxGFrame.Hotfixer.HotfixEvent;
using OxGFrame.Hotfixer.HotfixFsm;
using OxGKit.LoggingSystem;
using OxGKit.Utilities.Timer;
using UniFramework.Event;
using UnityEngine;

public class HotfixStage : GSIBase
{
    private enum Hotfix
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

    private Hotfix _hotfix = Hotfix.NONE;
    private RealTimer _realTimer;

    public async override UniTask OnCreate()
    {
        // Init Hotfix Events
        this._InitHotfixEvents();

        this._realTimer = new RealTimer();
    }

    public async override UniTask OnEnter()
    {
        this._hotfix = Hotfix.INIT_TIMER;
    }

    public override void OnUpdate(float dt = 0.0f)
    {
        switch (this._hotfix)
        {
            case Hotfix.INIT_TIMER:
                // Set buffer timer (doing background)
                this._realTimer.Reset();
                this._realTimer.Play();
                this._realTimer.SetTimer(3f);

                // Change next step immediately
                this._hotfix = Hotfix.START_CHECK_HOTFIX;
                break;

            // Start check hotfix while logo showing (doing background)
            case Hotfix.START_CHECK_HOTFIX:
                // Do hotfix check
                Hotfixers.CheckHotfix
                (
                    HotfixPkg,
                    new string[]
                    {
                        "UniTask.dll",
                        "mscorlib.dll",
                        "System.Core.dll",
                        "Newtonsoft.Json.dll",
                        "OxGFrame.CenterFrame.Runtime.dll"
                    },
                    new string[]
                    {
                        "FlappyBird.Hotfix.Runtime.dll"
                    }
                );

                // Change next step immediately
                this._hotfix = Hotfix.WAITING_FOR_HOTFIX;
                break;

            // Waiting for hotfix are all done
            case Hotfix.WAITING_FOR_HOTFIX:
                // Check hotfix are all done per frame rate
                if (Hotfixers.IsDone())
                {
                    this._hotfix = Hotfix.WAITING_FOR_BUFFER_TIME;
                    Logging.Print<MLogger>("<color=#7bff9e>Hotfix finished.</color>");
                }
                break;

            case Hotfix.WAITING_FOR_BUFFER_TIME:
                // If buffer timeout
                if (this._realTimer.IsTimerTimeout())
                {
                    // Set timer for outro anim
                    this._realTimer.SetTimer(1f);

                    // Close LogoUI
                    CoreFrames.UIFrame.Close(LogoStage.LogoUI);

                    // If after hotfix loaded and buffer timeout, will change next step
                    this._hotfix = Hotfix.LOAD_HOTFIX_MAIN_SCENE;
                }
                break;

            // Load hotfix main scene
            case Hotfix.LOAD_HOTFIX_MAIN_SCENE:
                // If outro buffer timeout
                if (this._realTimer.IsTimerTimeout())
                {
                    UniTask.Void(async () =>
                    {
                        this.StopUpdate();

                        // Start load single scene from HotfixPackage
                        await CoreFrames.USFrame.LoadSingleSceneAsync(HotfixPkg, HMain);

                        // Hotfix done
                        this._hotfix = Hotfix.DONE;
                    });
                }
                break;

            // Nothing to do
            case Hotfix.DONE:
                break;
        }
    }

    public override void OnExit()
    {
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
