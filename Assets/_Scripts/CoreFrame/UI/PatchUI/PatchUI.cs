using OxGFrame.CoreFrame.UIFrame;
using Cysharp.Threading.Tasks;
using OxGFrame.AssetLoader.PatchEvent;
using OxGFrame.AssetLoader.PatchFsm;
using OxGFrame.AssetLoader.Utility;
using OxGFrame.AssetLoader;
using UniFramework.Event;
using UnityEngine.UI;
using OxGKit.Utilities.Button;
using UnityEngine;
using System;
using TMPro;
using OxGKit.LoggingSystem;

public class PatchUI : UIBase
{
    // Use _Node@XXX to Bind

    #region Binding Components
    protected GameObject _progressGroup;
    protected TMP_Text _dlInfoTmpTxt;
    protected TMP_Text _dlSpeedTmpTxt;
    protected Slider _progressSld;
    protected TMP_Text _msgTmpTxt;
    protected ButtonPlus _repairBtnPlus;

    /// <summary>
    /// Auto Binding Section
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

    public override void OnCreate()
    {
        /**
         * Do Somethings Init Once In Here
         */
    }

    protected override async UniTask OnPreShow()
    {
        /**
         * On Pre-Show With Async
         */
    }

    protected override void OnPreClose()
    {
        /**
         * On Pre-Close
         */
    }

    protected override void OnBind()
    {
        this._InitEvents();
        this._InitPatchEvents();
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

    public override void OnReceiveAndRefresh(object obj = null)
    {
        /**
         * Do Refresh Once After Data Receive
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
        /**
         * Do Somethings on close (Close)
         */
    }

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
            // Show Patch Failed Retry UI
            this._ShowRetryEvent(0);
        }
        else if (message is PatchEvents.PatchFsmState)
        {
            // Display Patch State Msg
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
            // Show Go To App Store Confirm UI (add below event on confirm button)
            AssetPatcher.GoToAppStore();
        }
        else if (message is PatchEvents.PatchAppVersionUpdateFailed)
        {
            // Show App Version Update Failed Retry UI
            this._ShowRetryEvent(1);
        }
        else if (message is PatchEvents.PatchInitPatchModeFailed)
        {
            // Show Patch Init Patch Failed Retry UI
            this._ShowRetryEvent(2);
        }
        else if (message is PatchEvents.PatchVersionUpdateFailed)
        {
            // Show Patch Version Update Failed Retry UI
            this._ShowRetryEvent(3);
        }
        else if (message is PatchEvents.PatchManifestUpdateFailed)
        {
            // Show Patch Manifest Update Failed Retry UI
            this._ShowRetryEvent(4);
        }
        else if (message is PatchEvents.PatchCreateDownloader)
        {
            // Show GroupInfos UI for user to choose which one they want to download

            // Node: Recommend foreach GroupInfos to find max size and check user disk space

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
            // Show Disk Not Enough Space Retry UI

            // Note: You can retry create downloader again (unless, user frees up space) or submit Application.Quit event!!!

            // Here use action type is 6 (Application.Quit)
            this._ShowRetryEvent(6, message);
        }
        else if (message is PatchEvents.PatchDownloadProgression)
        {
            #region Download Progression
            // Receive Progression
            var downloadInfo = message as PatchEvents.PatchDownloadProgression;
            Logging.Print<MLogger>
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
            // Show Patch Download Files Failed Retry UI
            this._ShowRetryEvent(5);
        }
        else if (message is PatchEvents.PatchDownloadCanceled)
        {
            // Show Patch Download Canceled Retry UI
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
        string totalSize = BundleUtility.GetBytesToString((ulong)totalBytes);
        string dlSize = BundleUtility.GetBytesToString((ulong)dlBytes);
        string percentage = (progress * 100).ToString("f1");
        this._dlInfoTmpTxt.text = $"Downloading: {percentage}%({dlSize}/{totalSize})";

        // Download speed info
        long patchBytes = (long)totalBytes;
        long remainingSeconds = (patchBytes - dlBytes) / ((dlSpeedBytes <= 0) ? 1 : dlSpeedBytes);
        TimeSpan result = TimeSpan.FromSeconds(remainingSeconds);
        string remainingTime = result.ToString(@"hh\:mm\:ss");
        string dlSpeedSize = BundleUtility.GetSpeedBytesToString((ulong)dlSpeedBytes);
        this._dlSpeedTmpTxt.text = $"Remaining: {remainingTime}({dlSpeedSize})";

        // Progression Slider
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
