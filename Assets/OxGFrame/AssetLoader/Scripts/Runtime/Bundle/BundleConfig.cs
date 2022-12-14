using AssetLoader.Utility;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace OxGFrame.AssetLoader.Bundle
{
    public static class BundleConfig
    {
        public class CryptogramType
        {
            public const string NONE = "NONE";
            public const string OFFSET = "OFFSET";
            public const string XOR = "XOR";
            public const string HTXOR = "HTXOR";
            public const string AES = "AES";
        }

        #region 執行配置
        /// <summary>
        /// 啟用 Editor 中的 AssetDatabase 讀取資源模式
        /// </summary>
        public static bool assetDatabaseMode = true;

        /// <summary>
        /// 離線模式 Bundle (僅讀取 StreamingAssets 中的資源與配置檔)
        /// </summary>
        public static bool offlineMode = false;

        /// <summary>
        /// 啟用文件流 (較少內存)
        /// </summary>
        public static bool bundleStreamMode = true;

        /// <summary>
        /// 啟用使用 MD5 加密的包名
        /// </summary>
        public static bool readMd5BundleName = true;

        /// <summary>
        /// 預設最大切片大小 (16 MB)
        /// </summary>
        public const long defaultMaxDownloadSliceSize = 1 << 24;
        /// <summary>
        /// 檔案下載最大切片大小, 透過切片方式下載單個大檔, 可以避免內存佔用太大導致內存不足問題 (*不建議設置過大)
        /// </summary>
        public static long maxDownloadSliceSize = defaultMaxDownloadSliceSize;

        /// <summary>
        /// 預設解壓縮緩存大小 (64 KB)
        /// </summary>
        public const int defaultUnzipBufferSize = 1 << 16;
        /// <summary>
        /// 解壓縮緩存大小 (緩存愈大解壓愈快)
        /// </summary>
        public static int unzipBufferSize = defaultUnzipBufferSize;

        /// <summary>
        /// 預設壓縮包名稱
        /// </summary>
        public const string defaultZipFileName = "abzip";
        /// <summary>
        /// 壓縮包名稱
        /// </summary>
        public static string zipFileName = defaultZipFileName;

        /// <summary>
        /// 啟用 MD5 加密壓縮包名稱
        /// </summary>
        public static bool md5ForZipFileName = true;

        /// <summary>
        /// 解壓縮密碼
        /// </summary>
        public static string unzipPassword = string.Empty;

        /// <summary>
        /// 解密 Key, [NONE], [OFFSET, dummySize], [XOR, key], [HTXOR, hKey, tKey], [AES, key, iv] => ex: "None" or "offset, 12" or "xor, 23" or "htxor, 34, 45" or "aes, key, iv"
        /// </summary>
        private static string _cryptogram;
        public static string[] cryptogramArgs
        {
            get
            {
                if (string.IsNullOrEmpty(_cryptogram)) return new string[] { CryptogramType.NONE };
                else
                {
                    string[] args = _cryptogram.Trim().Split(',');
                    for (int i = 0; i < args.Length; i++)
                    {
                        args[i] = args[i].Trim();
                    }
                    return args;
                }
            }
        }

        /// <summary>
        /// 預設內部 Manifest 檔案名稱 (imf = Internal Manifest)
        /// </summary>
        public const string defaultInternalManifestName = "imf";

        /// <summary>
        /// 預設外部 Manifest 檔案名稱 (emf = External Manifest)
        /// </summary>
        public const string defaultExternalManifestName = "emf";

        /// <summary>
        /// 內部 Manifest 檔案名稱 (imf = Internal Manifest)
        /// </summary>
        public static string internalManifestName = defaultInternalManifestName;

        /// <summary>
        /// 外部 Manifest 檔案名稱 (emf = External Manifest)
        /// </summary>
        public static string externalManifestName = defaultExternalManifestName;
        #endregion

        #region 常數配置
        // 配置檔中的 KEY
        public const string APP_VERSION = "APP_VERSION";
        public const string RES_VERSION = "RES_VERSION";

        // 配置檔
        public const string cfgExtension = "";                // 自行輸入(.json), 空字串表示無副檔名
        public const string bakCfgExtension = ".bak";         // 備份配置檔副檔名
        public readonly static string bundleCfgName = "bcfg"; // 配置檔的名稱 
        public readonly static string recordCfgName = "rcfg"; // 記錄配置檔的名稱

        /**
         * url_cfg format following
         * bundle_ip <IP>
         * store_link <URL>
         * # => comment
         */

        // 佈署配置檔中的 KEY
        public const string BUNDLE_IP = "bundle_ip";
        public const string STORE_LINK = "store_link";

        // 佈署配置檔
        public const string bundleUrlFilePathName = "burlcfg.txt";

        // Bundle 平台路徑
        public const string bundleDir = "/AssetBundles";  // Build 目錄
        public const string exportDir = "/ExportBundles"; // Export 目錄
        public const string winDir = "/win";              // Windows
        public const string osxDir = "/osx";              // Mac OSX
        public const string androidDir = "/android";      // Android
        public const string iosDir = "/ios";              // iOS
        public const string h5Dir = "/h5";                // WebGL
        #endregion

        /// <summary>
        /// 初始 Bundle 解密參數
        /// </summary>
        /// <param name="cryptogram"></param>
        public static void InitCryptogram(string cryptogram)
        {
            _cryptogram = cryptogram;
        }

        /// <summary>
        /// 取得 burlcfg.txt 佈署配置檔的數據
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async UniTask<string> GetValueFromUrlCfg(string key)
        {
            string pathName = Path.Combine(GetRequestStreamingAssetsPath(), bundleUrlFilePathName);
            var content = await BundleUtility.FileRequestString(pathName);
            var allWords = content.Split('\n');
            var lines = new List<string>(allWords);
            var fileMap = new Dictionary<string, string>();
            foreach (var readLine in lines)
            {
                if (readLine.IndexOf('#') != -1 && readLine[0] == '#') continue;
                var args = readLine.Split(' ');
                if (args.Length >= 2)
                {
                    if (!fileMap.ContainsKey(args[0])) fileMap.Add(args[0], args[1].Replace("\n", "").Replace("\r", ""));
                }
            }

            fileMap.TryGetValue(key, out string value);
            return value;
        }

        /// <summary>
        /// 從設定檔中取得 StoreLink
        /// </summary>
        /// <returns></returns>
        public static async UniTask<string> GetAppStoreLink()
        {
            return await GetValueFromUrlCfg(STORE_LINK);
        }

        /// <summary>
        /// 取得打包路徑
        /// </summary>
        /// <returns></returns>
        public static string GetBuildBundlePath()
        {
#if UNITY_STANDALONE_WIN
            return Path.Combine(Application.dataPath, $"..{bundleDir}{winDir}");
#elif UNITY_STANDALONE_OSX
            return Path.Combine(Application.dataPath, $"..{bundleDir}{osxDir}");
#elif UNITY_ANDROID
            return Path.Combine(Application.dataPath, $"..{bundleDir}{androidDir}");
#elif UNITY_IOS
            return Path.Combine(Application.dataPath , $"..{bundleDir}{iosDir}");
#elif UNITY_WEBGL
            return Path.Combine(Application.dataPath, $"..{bundleDir}{h5Dir}");
#else
            return string.Empty;
#endif
            throw new System.Exception("ERROR Bundle PATH !!!");
        }

        /// <summary>
        /// 取得輸出路徑
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public static string GetExportBundlePath()
        {
#if UNITY_STANDALONE_WIN
            return Path.Combine(Application.dataPath, $"..{exportDir}{winDir}");
#elif UNITY_STANDALONE_OSX
            return Path.Combine(Application.dataPath, $"..{exportDir}{osxDir}");
#elif UNITY_ANDROID
            return Path.Combine(Application.dataPath, $"..{exportDir}{androidDir}");
#elif UNITY_IOS
            return Path.Combine(Application.dataPath , $"..{exportDir}{iosDir}");
#elif UNITY_WEBGL
            return Path.Combine(Application.dataPath, $"..{exportDir}{h5Dir}");
#else
            return string.Empty;
#endif
            throw new System.Exception("ERROR Export PATH !!!");
        }

        /// <summary>
        /// 取得本地持久化路徑 (下載的儲存路徑)
        /// </summary>
        /// <returns></returns>
        public static string GetLocalDlFileSaveDirectory()
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                // IOS 要用這個路徑，否則審核不過
                return Application.temporaryCachePath + exportDir;
            }

            // Android、PC 可以使用這個路徑
            return Application.persistentDataPath + exportDir;
        }

        /// <summary>
        /// 取得本地持久化路徑中的 BundleConfig 路徑 (下載的儲存路徑)
        /// </summary>
        /// <returns></returns>
        public static string GetLocalDlFileSaveBundleConfigPath()
        {
            return Path.Combine(GetLocalDlFileSaveDirectory(), $"{bundleCfgName}{cfgExtension}");
        }

        /// <summary>
        /// 取得 StreamingAssets 中的 BundleConfig 路徑
        /// </summary>
        /// <returns></returns>
        public static string GetStreamingAssetsBundleConfigPath()
        {
            return Path.Combine(GetRequestStreamingAssetsPath(), $"{bundleCfgName}{cfgExtension}");
        }

        /// <summary>
        /// 取得 UnityWebRequest StreamingAssets 路徑 (OSX and iOS 需要 + file://)
        /// </summary>
        /// <returns></returns>
        public static string GetRequestStreamingAssetsPath()
        {
#if UNITY_STANDALONE_OSX || UNITY_IOS
            return $"file://{Application.streamingAssetsPath}";
#else
            return Application.streamingAssetsPath;
#endif
        }

        /// <summary>
        /// 取得資源伺服器的 Bundle (URL)
        /// </summary>
        /// <returns></returns>
        public static async UniTask<string> GetServerBundleUrl()
        {
#if UNITY_STANDALONE_WIN
            return await GetValueFromUrlCfg(BUNDLE_IP) + $"{exportDir}{winDir}";
#elif UNITY_STANDALONE_OSX
            return await GetValueFromUrlCfg(BUNDLE_IP) + $"{exportDir}{osxDir}";
#elif UNITY_ANDROID
            return await GetValueFromUrlCfg(BUNDLE_IP) + $"{exportDir}{androidDir}";
#elif UNITY_IOS
            return await GetValueFromUrlCfg(BUNDLE_IP) + $"{exportDir}{iosDir}";
#elif UNITY_WEBGL
            return await GetValueFromUrlCfg(BUNDLE_IP) + $"{exportDir}{h5Dir}";
#else
            return string.Empty;
#endif
            throw new System.Exception("ERROR Server URL !!!");
        }

        /// <summary>
        /// 取得 Bundle Manifest 檔名 (判斷區分 Built-in or Patch)
        /// </summary>
        /// <returns></returns>
        public static string GetManifestFileName(bool inApp)
        {
            if (inApp) return internalManifestName;
            else return externalManifestName;
        }
    }
}
