using MyBox;
using UnityEngine;

namespace FlappyBird.Main.Runtime
{
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
            // Get CastCamera by self
            this._castCamera = this.GetComponent<Camera>();
            if (this._castCamera != null)
            {
                // Find UICamera by tag
                this._uiCamera = GameObject.FindGameObjectWithTag(overlayUICameraTag)?.GetComponent<Camera>();

                // Finally, set UICamera at last
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
                // Only do depth equlas set value depth
                if (this._castCamera.depth == this.autoByDepth)
                {
                    // Get frame count
                    int frameCount = Time.frameCount;

                    // Check per set value frame rate
                    if (frameCount % this.refreshPerFrameCount == 0)
                    {
                        // If CastCamera contains UICamera
                        if (this._castCamera.ContainsUniversalOverlayCamera(this._uiCamera))
                        {
                            // Find all CastCamera by tag
                            GameObject[] camGos = GameObject.FindGameObjectsWithTag(castCameraTag);
                            // If length > 1 represents including 2 above CastCamera (must auto remove UICamera overlay)
                            if (camGos != null && camGos.Length > 1)
                            {
                                // Remove UICamera from CastCamera
                                this._castCamera.RemoveUniversalOverlayCamera(this._uiCamera);
                            }
                        }
                        else
                        {
                            // Find all CastCamera by tag
                            GameObject[] camGos = GameObject.FindGameObjectsWithTag(castCameraTag);
                            // If length == 1 represents only remaining first CastCamera (must auto add UICamera overlay)
                            if (camGos != null && camGos.Length == 1)
                            {
                                // Add back UICamera to CastCamera
                                this._castCamera.AddUniversalOverlayCamera(this._uiCamera);
                            }
                        }
                    }
                }
            }
        }
    }
}