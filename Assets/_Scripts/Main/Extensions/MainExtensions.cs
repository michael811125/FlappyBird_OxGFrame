using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace FlappyBird.Main.Runtime
{
    public static class MainExtensions
    {
        #region Camera 擴充
        public static void AddUniversalOverlayCamera(this Camera camera, Camera overlayCamera)
        {
            if (camera == null) return;
            var cameraData = camera.GetUniversalAdditionalCameraData();
            cameraData.cameraStack.Add(overlayCamera);
        }

        public static void RemoveUniversalOverlayCamera(this Camera camera, Camera overlayCamera)
        {
            if (camera == null) return;
            var cameraData = camera.GetUniversalAdditionalCameraData();
            cameraData.cameraStack.Remove(overlayCamera);
        }

        public static bool ContainsUniversalOverlayCamera(this Camera camera, Camera overlayCamera)
        {
            if (camera == null) return false;
            var cameraData = camera.GetUniversalAdditionalCameraData();
            return cameraData.cameraStack.Contains(overlayCamera);
        }

        public static void ClearUniversalOverlayCameras(this Camera camera)
        {
            if (camera == null) return;
            var cameraData = camera.GetUniversalAdditionalCameraData();
            cameraData.cameraStack.Clear();
        }
        #endregion

        #region JObject 擴充
        public static T SelectToken<T>(this JObject jObject, params object[] keys)
        {
            // 路徑不能有空格                

            string path = "";
            for (int i = 0; i < keys.Length; i++)
            {
                if (i == (keys.Length - 1)) path += keys[i].ToString();
                else path += keys[i].ToString() + ".";
            }

            JToken value = jObject.SelectToken(path);
            if (value == null) return default;

            return value.Value<T>();
        }
        #endregion
    }
}