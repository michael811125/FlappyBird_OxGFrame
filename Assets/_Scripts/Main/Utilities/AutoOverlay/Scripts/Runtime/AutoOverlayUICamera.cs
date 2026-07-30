using MyBox;
using UnityEngine;

namespace FlappyBird.Main.Runtime
{
    /// <summary>
    /// Keeps the UI camera as the last overlay of every cast camera stack, so UI is
    /// always drawn on top no matter how many cameras are stacked.
    /// <para>
    /// 讓 UI 相機始終保持在各個 Cast Camera 堆疊的最後一層,
    /// 無論堆疊多少相機, UI 都會繪製在最上方。
    /// </para>
    /// </summary>
    public class AutoOverlayUICamera : MonoBehaviour
    {
        public string castCameraTag = "CastCamera";
        public string overlayUICameraTag = "UICamera";
        public bool autoOverlay = false;
        [ConditionalField(nameof(autoOverlay))]
        public int autoByDepth = 0;
        [ConditionalField(nameof(autoOverlay))]
        public int refreshPerFrameCount = 60;

        private Camera _castCamera;
        private Camera _uiCamera;

        private void Awake()
        {
            // Get the CastCamera on this object
            // 取得自身的 CastCamera
            this._castCamera = this.GetComponent<Camera>();
            if (this._castCamera != null)
            {
                // Find the UICamera by tag
                // 以 Tag 尋找 UICamera
                this._uiCamera = GameObject.FindGameObjectWithTag(overlayUICameraTag)?.GetComponent<Camera>();

                // Finally, put the UICamera last
                // 最後將 UICamera 排到最後
                if (!this._castCamera.ContainsUniversalOverlayCamera(this._uiCamera))
                {
                    this._castCamera.AddUniversalOverlayCamera(this._uiCamera);
                }
            }
        }

        private void Update()
        {
            if (autoOverlay)
            {
                // Only run when the depth equals the configured depth
                // 僅在 depth 等於設定值時執行
                if (this._castCamera.depth == this.autoByDepth)
                {
                    // Get the frame count
                    // 取得幀數
                    int frameCount = Time.frameCount;

                    // Check on the configured frame interval
                    // 依設定的幀間隔進行檢查
                    if (frameCount % this.refreshPerFrameCount == 0)
                    {
                        // If the CastCamera stack contains the UICamera
                        // 若 CastCamera 已包含 UICamera
                        if (this._castCamera.ContainsUniversalOverlayCamera(this._uiCamera))
                        {
                            // Find every CastCamera by tag
                            // 以 Tag 尋找所有 CastCamera
                            GameObject[] camGos = GameObject.FindGameObjectsWithTag(castCameraTag);
                            // Length > 1 means there are 2 or more CastCameras (the UICamera overlay must be removed automatically)
                            // 長度 > 1 代表有 2 個以上的 CastCamera (必須自動移除 UICamera overlay)
                            if (camGos != null && camGos.Length > 1)
                            {
                                // Remove the UICamera from the CastCamera
                                // 從 CastCamera 移除 UICamera
                                this._castCamera.RemoveUniversalOverlayCamera(this._uiCamera);
                            }
                        }
                        else
                        {
                            // Find every CastCamera by tag
                            // 以 Tag 尋找所有 CastCamera
                            GameObject[] camGos = GameObject.FindGameObjectsWithTag(castCameraTag);
                            // Length == 1 means only the first CastCamera remains (the UICamera overlay must be added back automatically)
                            // 長度 == 1 代表僅剩第一個 CastCamera (必須自動加回 UICamera overlay)
                            if (camGos != null && camGos.Length == 1)
                            {
                                // Add the UICamera back to the CastCamera
                                // 將 UICamera 加回 CastCamera
                                this._castCamera.AddUniversalOverlayCamera(this._uiCamera);
                            }
                        }
                    }
                }
            }
        }
    }
}