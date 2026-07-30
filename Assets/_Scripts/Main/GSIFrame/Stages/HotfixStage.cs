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
    /// <summary>
    /// [Main.Assembly] Hotfix stage. Runs the HybridCLR hotfix check while the logo is
    /// showing, then loads the hotfix main scene, which is where Hotfix.Assembly takes
    /// over. This is the hand-over point between the two assemblies.
    /// <para>
    /// [主工程] 熱更階段。在顯示商標的同時執行 HybridCLR 熱更檢查,
    /// 完成後載入熱更主場景, 由 Hotfix.Assembly 接手。
    /// 此處即為兩個 Assembly 的交接點。
    /// </para>
    /// </summary>
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

        /// <summary>
        /// Called once when the game stage is created.
        /// <para>遊戲階段建立時呼叫一次。</para>
        /// </summary>
        public async override UniTask OnCreate()
        {
            // Init the hotfix events
            // 初始化熱更事件
            this._InitHotfixEvents();

            this._realTimer = new RealTimer();
        }

        /// <summary>
        /// Called every time this game stage is entered.
        /// <para>每次進入此遊戲階段時呼叫。</para>
        /// </summary>
        public async override UniTask OnEnter()
        {
            this._step = HotfixStep.INIT_TIMER;
        }

        /// <summary>
        /// Called per frame while this game stage is running.
        /// <para>此遊戲階段運行期間每幀呼叫。</para>
        /// </summary>
        public override void OnUpdate(float dt = 0.0f)
        {
            switch (this._step)
            {
                case HotfixStep.INIT_TIMER:
                    // Set the buffer timer (runs in the background)
                    // 設定緩衝計時器 (於背景進行)
                    this._realTimer.Reset();
                    this._realTimer.Play();
                    this._realTimer.SetTimer(3f);

                    // Change to the next step immediately
                    // 立即切換下一步
                    this._step = HotfixStep.START_CHECK_HOTFIX;
                    break;

                // Start checking the hotfix while the logo is showing (runs in the background)
                // 顯示商標的同時開始檢查熱更 (於背景進行)
                case HotfixStep.START_CHECK_HOTFIX:
                    // Do the hotfix check, it can also read hotfixconfig.conf from StreamingAssets
                    // 進行熱更檢查, 亦可從 StreamingAssets 讀取 hotfixconfig.conf
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

                    // Change to the next step immediately
                    // 立即切換下一步
                    this._step = HotfixStep.WAITING_FOR_HOTFIX;
                    break;

                // Wait until the hotfix is completely done
                // 等待熱更全部完成
                case HotfixStep.WAITING_FOR_HOTFIX:
                    // Check per frame whether the hotfix is done
                    // 依幀率檢查熱更是否全部完成
                    if (Hotfixers.IsDone())
                    {
                        this._step = HotfixStep.WAITING_FOR_BUFFER_TIME;
                        Logging.PrintInfo<MLogger>("Hotfix finished.");
                    }
                    break;

                case HotfixStep.WAITING_FOR_BUFFER_TIME:
                    // If the buffer timed out
                    // 若緩衝時間已到
                    if (this._realTimer.IsTimerTimeout())
                    {
                        // Set the timer for the outro animation
                        // 設定結束動畫的計時器
                        this._realTimer.SetTimer(1f);

                        // Close LogoUI
                        // 關閉 LogoUI
                        CoreFrames.UIFrame.Close(LogoStage.LogoUI);

                        // Once the hotfix is loaded and the buffer times out, change to the next step
                        // 熱更載入完成且緩衝時間已到後, 切換下一步
                        this._step = HotfixStep.LOAD_HOTFIX_MAIN_SCENE;
                    }
                    break;

                // Load the hotfix main scene
                // 載入熱更主場景
                case HotfixStep.LOAD_HOTFIX_MAIN_SCENE:
                    // If the outro buffer timed out
                    // 若結束動畫的緩衝時間已到
                    if (this._realTimer.IsTimerTimeout())
                    {
                        this._LoadHotfixMainScene().Forget();
                    }
                    break;

                // Nothing to do
                // 無須處理
                case HotfixStep.DONE:
                    break;
            }
        }

        /// <summary>
        /// Called when leaving this game stage.
        /// <para>離開此遊戲階段時呼叫。</para>
        /// </summary>
        public override void OnExit()
        {
        }

        private async UniTask _LoadHotfixMainScene()
        {
            this.StopUpdate();

            // Start loading the single scene from HotfixPackage
            // 開始從 HotfixPackage 載入單一場景
            await CoreFrames.USFrame.LoadSingleSceneAsync(HotfixPkg, HMain);

            // Hotfix done
            // 熱更完成
            this._step = HotfixStep.DONE;
        }

        #region Hotfix Event
        private EventGroup _hotfixEvents = new EventGroup();
        private void _InitHotfixEvents()
        {
            // Hotfix events handled below:
            // 0. HotfixFsmState
            // 1. HotfixInitFailed
            // 2. HotfixUpdateFailed
            // 3. HotfixCreateDownloader
            // 4. HotfixDownloadProgression
            // 5. HotfixDownloadFailed
            // 以下處理的熱更事件:
            // 0. 熱更狀態機狀態
            // 1. 熱更初始化失敗
            // 2. 熱更更新失敗
            // 3. 建立熱更下載器
            // 4. 熱更下載進度
            // 5. 熱更下載失敗

            #region Add HotfixEvents Handle
            this._hotfixEvents.AddListener<HotfixEvents.HotfixFsmState>(this._OnHandleEventMessage);
            this._hotfixEvents.AddListener<HotfixEvents.HotfixInitFailed>(this._OnHandleEventMessage);
            this._hotfixEvents.AddListener<HotfixEvents.HotfixUpdateFailed>(this._OnHandleEventMessage);
            this._hotfixEvents.AddListener<HotfixEvents.HotfixCreateDownloader>(this._OnHandleEventMessage);
            this._hotfixEvents.AddListener<HotfixEvents.HotfixDownloadProgression>(this._OnHandleEventMessage);
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
            else if (message is HotfixEvents.HotfixCreateDownloader)
            {
                HotfixUserEvents.UserBeginDownload.SendEventMessage();
            }
            else if (message is HotfixEvents.HotfixDownloadProgression)
            {
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