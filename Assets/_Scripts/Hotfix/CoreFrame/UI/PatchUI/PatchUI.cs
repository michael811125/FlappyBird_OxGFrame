using Cysharp.Threading.Tasks;
using OxGFrame.AssetLoader;
using OxGFrame.AssetLoader.PatchEvent;
using OxGFrame.AssetLoader.PatchFsm;
using OxGFrame.AssetLoader.Utility;
using OxGFrame.CoreFrame.UIFrame;
using OxGKit.LoggingSystem;
using OxGKit.ButtonSystem;
using System;
using TMPro;
using UniFramework.Event;
using UnityEngine;
using UnityEngine.UI;

namespace FlappyBird.Hotfix.Runtime
{
    /// <summary>
    /// Patch UI. Subscribes to the whole PackageEvents set and renders download size,
    /// speed and progress, plus the retry flows for every failure case.
    /// <para>
    /// 補丁更新介面。訂閱完整的 PackageEvents, 呈現下載容量、速度與進度,
    /// 並處理各種失敗情境的重試流程。
    /// </para>
    /// </summary>
    public class PatchUI : UIBase
    {
        // Use _Node@XXX on the prefab to bind nodes
        // 於 Prefab 上使用 _Node@XXX 命名進行節點綁定

        #region Binding Components
        protected GameObject _progressGroup;
        protected TMP_Text _dlInfoTmpTxt;
        protected TMP_Text _dlSpeedTmpTxt;
        protected Slider _progressSld;
        protected TMP_Text _msgTmpTxt;
        protected ButtonPlus _repairBtnPlus;

        /// <summary>
        /// Auto binding section
        /// <para>自動綁定區塊</para>
        /// </summary>
        protected override void OnAutoBind()
        {
            base.OnAutoBind();
            this._progressGroup = this.collector.GetNode("ProgressGroup");
            this._dlInfoTmpTxt = this.collector.GetNodeComponent<TMP_Text>("DlInfo*TmpTxt");
            this._dlSpeedTmpTxt = this.collector.GetNodeComponent<TMP_Text>("DlSpeed*TmpTxt");
            this._progressSld = this.collector.GetNodeComponent<Slider>("Progress*Sld");
            this._msgTmpTxt = this.collector.GetNodeComponent<TMP_Text>("Msg*TmpTxt");
            this._repairBtnPlus = this.collector.GetNodeComponent<ButtonPlus>("Repair*BtnPlus");
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
             * On pre-show with async
             * 以非同步進行顯示前處理
             */
        }

        /// <summary>
        /// Called before closing, close sub objects here.
        /// <para>關閉前呼叫, 可在此關閉子物件。</para>
        /// </summary>
        protected override void OnPreClose()
        {
            /**
             * On pre-close
             * 進行關閉前處理
             */
        }

        /// <summary>
        /// Bind component events here.
        /// <para>在此處綁定元件事件。</para>
        /// </summary>
        protected override void OnBind()
        {
            this._InitEvents();
            this._InitPatchEvents();
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
            this._patchEvents.RemoveAllListener();
        }

        private EventGroup _patchEvents = new EventGroup();

        private void _InitEvents()
        {
            this._repairBtnPlus.On(() =>
            {
                UIEventCenter.Find<EDoubleCheckUI>().Emit
                (
                    "Notice",
                    "Do you want to execute repair？",
                    AssetPatcher.Repair
                );
            }, null);
        }

        #region Patch Event
        private void _InitPatchEvents()
        {
            // Patch events handled below:
            // 0. PatchRepairFailed
            // 1. PatchFsmState
            // 2. PatchGoToAppStore
            // 3. PatchAppVersionUpdateFailed
            // 4. PatchInitPatchModeFailed
            // 5. PatchVersionUpdateFailed
            // 6. PatchManifestUpdateFailed
            // 7. PatchCreateDownloader
            // 8. PatchCheckDiskNotEnoughSpace
            // 9. PatchDownloadProgression
            // 10. PatchDownloadFailed
            // 11. PatchDownloadCanceled
            // 以下處理的補丁事件:
            // 0. 補丁修復失敗
            // 1. 補丁狀態機狀態
            // 2. 前往應用商店
            // 3. App 版本更新失敗
            // 4. 補丁模式初始化失敗
            // 5. 補丁版本更新失敗
            // 6. 補丁清單更新失敗
            // 7. 建立下載器
            // 8. 檢查磁碟空間不足
            // 9. 補丁下載進度
            // 10. 補丁下載失敗
            // 11. 補丁下載取消

            #region Add PatchEvents Handle
            this._patchEvents.AddListener<PatchEvents.PatchRepairFailed>(this._OnHandleEventMessage);
            this._patchEvents.AddListener<PatchEvents.PatchFsmState>(this._OnHandleEventMessage);
            this._patchEvents.AddListener<PatchEvents.PatchGoToAppStore>(this._OnHandleEventMessage);
            this._patchEvents.AddListener<PatchEvents.PatchAppVersionUpdateFailed>(this._OnHandleEventMessage);
            this._patchEvents.AddListener<PatchEvents.PatchInitPatchModeFailed>(this._OnHandleEventMessage);
            this._patchEvents.AddListener<PatchEvents.PatchVersionUpdateFailed>(this._OnHandleEventMessage);
            this._patchEvents.AddListener<PatchEvents.PatchManifestUpdateFailed>(this._OnHandleEventMessage);
            this._patchEvents.AddListener<PatchEvents.PatchCreateDownloader>(this._OnHandleEventMessage);
            this._patchEvents.AddListener<PatchEvents.PatchCheckDiskNotEnoughSpace>(this._OnHandleEventMessage);
            this._patchEvents.AddListener<PatchEvents.PatchDownloadProgression>(this._OnHandleEventMessage);
            this._patchEvents.AddListener<PatchEvents.PatchDownloadFailed>(this._OnHandleEventMessage);
            this._patchEvents.AddListener<PatchEvents.PatchDownloadCanceled>(this._OnHandleEventMessage);
            #endregion
        }

        private void _OnHandleEventMessage(IEventMessage message)
        {
            if (message is PatchEvents.PatchRepairFailed)
            {
                // Show the patch failed retry UI
                // 顯示補丁失敗重試 UI
                this._ShowRetryEvent(0);
            }
            else if (message is PatchEvents.PatchFsmState)
            {
                // Display the patch state message
                // 顯示補丁狀態訊息
                #region PatchFsmState
                PatchEvents.PatchFsmState msgData = message as PatchEvents.PatchFsmState;

                switch (msgData.stateNode)
                {
                    case PatchFsmStates.FsmPatchRepair:
                        this._msgTmpTxt.text = "Patch Repair";
                        break;
                    case PatchFsmStates.FsmPatchPrepare:
                        this._msgTmpTxt.text = "Check Update";
                        if (this._progressGroup.activeSelf) this._progressGroup.SetActive(false);
                        this._progressSld.value = 0f;
                        break;
                    case PatchFsmStates.FsmAppVersionUpdate:
                        this._msgTmpTxt.text = "Check App Version";
                        break;
                    case PatchFsmStates.FsmInitPatchMode:
                        this._msgTmpTxt.text = "Setup Configure";
                        break;
                    case PatchFsmStates.FsmPatchVersionUpdate:
                        this._msgTmpTxt.text = "Check Patch Version";
                        break;
                    case PatchFsmStates.FsmPatchManifestUpdate:
                        this._msgTmpTxt.text = "Update Configure";
                        break;
                    case PatchFsmStates.FsmCreateDownloader:
                        this._msgTmpTxt.text = "Check Patch";
                        break;
                    case PatchFsmStates.FsmBeginDownload:
                        this._msgTmpTxt.text = "Download Patch";
                        if (!this._progressGroup.activeSelf) this._progressGroup.SetActive(true);
                        break;
                    case PatchFsmStates.FsmDownloadOver:
                        this._msgTmpTxt.text = "Done";
                        break;
                    case PatchFsmStates.FsmClearCache:
                        this._msgTmpTxt.text = "Clear Cache";
                        break;
                    case PatchFsmStates.FsmPatchDone:
                        this._msgTmpTxt.text = "Finished";
                        if (this._progressGroup.activeSelf) this._progressGroup.SetActive(false);
                        break;
                }
                #endregion
            }
            else if (message is PatchEvents.PatchGoToAppStore)
            {
                // Show the go-to-app-store confirm UI (hook the event below onto the confirm button)
                // 顯示前往應用商店的確認 UI (將下方事件掛到確認按鈕)
                AssetPatcher.GoToAppStore();
            }
            else if (message is PatchEvents.PatchAppVersionUpdateFailed)
            {
                // Show the app version update failed retry UI
                // 顯示 App 版本更新失敗重試 UI
                this._ShowRetryEvent(1);
            }
            else if (message is PatchEvents.PatchInitPatchModeFailed)
            {
                // Show the patch init failed retry UI
                // 顯示補丁初始化失敗重試 UI
                this._ShowRetryEvent(2);
            }
            else if (message is PatchEvents.PatchVersionUpdateFailed)
            {
                // Show the patch version update failed retry UI
                // 顯示補丁版本更新失敗重試 UI
                this._ShowRetryEvent(3);
            }
            else if (message is PatchEvents.PatchManifestUpdateFailed)
            {
                // Show the patch manifest update failed retry UI
                // 顯示補丁清單更新失敗重試 UI
                this._ShowRetryEvent(4);
            }
            else if (message is PatchEvents.PatchCreateDownloader)
            {
                // Show the GroupInfos UI so the user can choose what to download
                // 顯示 GroupInfos UI 讓使用者選擇要下載的內容

                // Note: iterating GroupInfos to find the max size and check the user's disk space is recommended
                // 注意: 建議遍歷 GroupInfos 找出最大容量並檢查使用者磁碟空間

                #region Show GroupInfos
                var msgData = message as PatchEvents.PatchCreateDownloader;
                UIEventCenter.Find<EDoubleCheckUI>().Emit
                (
                    "Hint",
                    $"Patch Size: {BundleUtility.GetBytesToString((ulong)msgData.groupInfos[0].totalBytes)}\nDo you want to download?\n(Mobile data will be consumed during the download process. It is recommended to use Wi-Fi)",
                    () =>
                    {
                        PatchUserEvents.UserBeginDownload.SendEventMessage(msgData.groupInfos[0]);
                    },
                    () =>
                    {
                        Application.Quit();
                    }
                );
                #endregion
            }
            else if (message is PatchEvents.PatchCheckDiskNotEnoughSpace)
            {
                // Show the disk-not-enough-space retry UI
                // 顯示磁碟空間不足重試 UI

                // Note: you can retry creating the downloader (once the user frees up space) or fire an Application.Quit event
                // 注意: 可以重新建立下載器 (需使用者先釋放空間), 或送出 Application.Quit 事件

                // The action type used here is 6 (Application.Quit)
                // 此處使用的 action type 為 6 (Application.Quit)
                this._ShowRetryEvent(6, message);
            }
            else if (message is PatchEvents.PatchDownloadProgression)
            {
                #region Download Progression
                // Receive the download progression
                // 接收下載進度
                var downloadInfo = message as PatchEvents.PatchDownloadProgression;
                Logging.Print<HLogger>
                (
                    $"Progress: {downloadInfo.progress}, " +
                    $"TotalCount: {downloadInfo.totalDownloadCount}, " +
                    $"TotalSize: {BundleUtility.GetBytesToString((ulong)downloadInfo.totalDownloadSizeBytes)}, " +
                    $"CurrentCount: {downloadInfo.currentDownloadCount}, " +
                    $"CurrentSize: {BundleUtility.GetBytesToString((ulong)downloadInfo.currentDownloadSizeBytes)}" +
                    $"DownloadSpeed: {BundleUtility.GetSpeedBytesToString((ulong)downloadInfo.downloadSpeedBytes)}"
                );

                this._UpdateDownloadInfo
                (
                    downloadInfo.progress,
                    downloadInfo.currentDownloadCount,
                    downloadInfo.currentDownloadSizeBytes,
                    downloadInfo.totalDownloadCount,
                    downloadInfo.totalDownloadSizeBytes,
                    downloadInfo.downloadSpeedBytes
                );
                #endregion
            }
            else if (message is PatchEvents.PatchDownloadFailed)
            {
                // Show the download files failed retry UI
                // 顯示檔案下載失敗重試 UI
                this._ShowRetryEvent(5);
            }
            else if (message is PatchEvents.PatchDownloadCanceled)
            {
                // Show the download canceled retry UI
                // 顯示下載取消重試 UI
                this._ShowRetryEvent(5);
            }
            else
            {
                throw new System.NotImplementedException($"{message.GetType()}");
            }
        }

        private void _UpdateDownloadInfo(float progress, int dlCount, long dlBytes, int totalCount, long totalBytes, long dlSpeedBytes)
        {
            if (!this._progressGroup.activeSelf) this._progressGroup.SetActive(true);

            if (this._dlInfoTmpTxt == null || this._dlSpeedTmpTxt == null) return;

            // Download size info
            // 下載容量資訊
            string totalSize = BundleUtility.GetBytesToString((ulong)totalBytes);
            string dlSize = BundleUtility.GetBytesToString((ulong)dlBytes);
            string percentage = (progress * 100).ToString("f1");
            this._dlInfoTmpTxt.text = $"Downloading: {percentage}%({dlSize}/{totalSize})";

            // Download speed info
            // 下載速度資訊
            long patchBytes = (long)totalBytes;
            long remainingSeconds = (patchBytes - dlBytes) / ((dlSpeedBytes <= 0) ? 1 : dlSpeedBytes);
            TimeSpan result = TimeSpan.FromSeconds(remainingSeconds);
            string remainingTime = result.ToString(@"hh\:mm\:ss");
            string dlSpeedSize = BundleUtility.GetSpeedBytesToString((ulong)dlSpeedBytes);
            this._dlSpeedTmpTxt.text = $"Remaining: {remainingTime}({dlSpeedSize})";

            // Progression slider
            // 進度條
            this._progressSld.value = progress;
        }
        #endregion

        private void _ShowRetryEvent(int retryEvent, IEventMessage eventMessage = null)
        {
            string title = "Notice";
            string msg = string.Empty;
            Action retryAction = null;

            switch (retryEvent)
            {
                case 0:
                    msg = "Repair failed.\nDo you want to retry?";
                    retryAction = PatchUserEvents.UserTryPatchRepair.SendEventMessage;
                    break;

                case 1:
                    msg = "App update failed.\nDo you want to retry?";
                    retryAction = PatchUserEvents.UserTryAppVersionUpdate.SendEventMessage;
                    break;

                case 2:
                    msg = "Patch init failed.\nDo you want to retry?";
                    retryAction = PatchUserEvents.UserTryInitPatchMode.SendEventMessage;
                    break;

                case 3:
                    msg = "Patch update failed.\nDo you want to retry?";
                    retryAction = PatchUserEvents.UserTryPatchVersionUpdate.SendEventMessage;
                    break;

                case 4:
                    msg = "Config update failed.\nDo you want to retry?";
                    retryAction = PatchUserEvents.UserTryPatchManifestUpdate.SendEventMessage;
                    break;

                case 5:
                    msg = "Patch download failed.\nDo you want to retry?";
                    retryAction = PatchUserEvents.UserTryCreateDownloader.SendEventMessage;
                    break;

                case 6:
                    int availableMegabytes = 0;
                    ulong patchTotalBytes = 0;
                    if (eventMessage != null)
                    {
                        var msgData = eventMessage as PatchEvents.PatchCheckDiskNotEnoughSpace;
                        availableMegabytes = msgData.availableMegabytes;
                        patchTotalBytes = msgData.patchTotalBytes;
                    }
                    msg = $"Disk not enough space!!!\nAvailable disk space size: {BundleUtility.GetMegabytesToString(availableMegabytes)}\nPatch total size: {BundleUtility.GetBytesToString(patchTotalBytes)}";
                    Action quit = () =>
                    {
                        // Application quit
                        // 結束應用程式
#if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
#else
                        Application.Quit();
#endif
                    };
                    retryAction = quit;
                    break;
            }

            UIEventCenter.Find<EDoubleCheckUI>().Emit(title, msg, retryAction);
        }
    }
}