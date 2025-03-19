using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OxGKit.ButtonSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FlappyBird.Hotfix.Runtime
{
    public static class HotfixExtensions
    {
        public delegate void ItemAndIndex<in T>(T item, int idx);

        #region Array 擴充
        public static void ForEach<T>(this T[] array, ItemAndIndex<T> itemAndIndex)
        {
            for (int i = 0; i < array.Length; i++)
            {
                itemAndIndex(array[i], i);
            }
        }
        #endregion

        #region List 擴充
        /// <summary>
        /// Return last of list element with remove
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T Pop<T>(this List<T> list)
        {
            if (list.Count > 0)
            {
                T item = list[list.Count - 1];
                list.RemoveAt(list.Count - 1);
                return item;
            }

            return default;
        }

        /// <summary>
        /// Return fist of list element with remove
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T Shift<T>(this List<T> list)
        {
            if (list.Count > 0)
            {
                T item = list[0];
                list.RemoveAt(0);
                return item;
            }

            return default;
        }

        public static void ForEach<T>(this List<T> list, ItemAndIndex<T> itemAndIndex)
        {
            for (int i = 0; i < list.Count; i++)
            {
                itemAndIndex(list[i], i);
            }
        }

        public static T GetLastElement<T>(this List<T> list)
        {
            return (list.Count < 1) ? default : list[list.Count - 1];
        }

        public static void Move<T>(this List<T> list, int oldIndex, int newIndex)
        {
            var item = list[oldIndex];

            list.RemoveAt(oldIndex);
            // the actual index could have shifted due to the removal
            if (newIndex > oldIndex) newIndex--;
            list.Insert(newIndex, item);
        }

        public static void Move<T>(this List<T> list, T item, int newIndex)
        {
            if (item != null)
            {
                var oldIndex = list.IndexOf(item);
                if (oldIndex > -1)
                {
                    list.RemoveAt(oldIndex);
                    // the actual index could have shifted due to the removal
                    if (newIndex > oldIndex) newIndex--;
                    list.Insert(newIndex, item);
                }
            }
        }
        #endregion

        #region Dictionary 擴充
        public static TValue GetValue<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
        {
            dict.TryGetValue(key, out TValue output);
            return output;
        }

        public static void AddRangeOverride<TKey, TValue>(this IDictionary<TKey, TValue> dict, IDictionary<TKey, TValue> dictToAdd)
        {
            dictToAdd.ForEach(x => dict[x.Key] = x.Value);
        }

        public static void AddRangeNewOnly<TKey, TValue>(this IDictionary<TKey, TValue> dict, IDictionary<TKey, TValue> dictToAdd)
        {
            dictToAdd.ForEach(x => { if (!dict.ContainsKey(x.Key)) dict.Add(x.Key, x.Value); });
        }

        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dict, IDictionary<TKey, TValue> dictToAdd)
        {
            dictToAdd.ForEach(x => dict.Add(x.Key, x.Value));
        }

        public static bool ContainsKeys<TKey, TValue>(this IDictionary<TKey, TValue> dict, IEnumerable<TKey> keys)
        {
            bool result = false;
            keys.ForEachOrBreak((x) => { result = dict.ContainsKey(x); return result; });
            return result;
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
                action(item);
        }

        public static void ForEachOrBreak<T>(this IEnumerable<T> source, Func<T, bool> func)
        {
            foreach (var item in source)
            {
                bool result = func(item);
                if (result) break;
            }
        }
        #endregion

        #region Transform 擴充
        /// <summary>
        /// Destroy all children
        /// </summary>
        /// <param name="trans"></param>
        public static void DestroyAllChildren(this Transform parent)
        {
            foreach (Transform child in parent)
            {
                if (child != null)
                    UnityEngine.Object.Destroy(child.gameObject);
            }
        }

        /// <summary>
        /// Destroy all children Async
        /// </summary>
        /// <param name="trans"></param>
        /// <returns></returns>
        public static async UniTask DestroyAllChildrenAsync(this Transform parent)
        {
            foreach (Transform child in parent)
            {
                if (child != null)
                {
                    UnityEngine.Object.Destroy(child.gameObject);
                    await UniTask.Yield();
                }
            }
        }

        public static void SetChildrenActive(this Transform parent, bool active)
        {
            foreach (Transform child in parent)
            {
                if (child != null)
                    child.gameObject.SetActive(active);
            }
        }

        public static Transform[] Children(this Transform parent)
        {
            List<Transform> children = new List<Transform>();

            for (int i = 0; i < parent.childCount; i++)
            {
                children.Add(parent.GetChild(i));
            }

            return children.ToArray();
        }
        #endregion

        #region string 擴充
        /// <summary>
        /// Get the string slice between the two indexes.
        /// Inclusive for start index, exclusive for end index.
        /// </summary>
        public static string Slice(this string source, int start, int end)
        {
            if (end < 0) // Keep this for negative end support
            {
                end = source.Length + end;
            }
            int len = end - start;               // Calculate length
            return source.Substring(start, len); // Return Substring of length
        }

        public static byte ToByte(this string str)
        {
            return Convert.ToByte(str);
        }

        /// <summary>
        /// 2 bytes
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static short ToShort(this string str)
        {
            return Convert.ToInt16(str);
        }

        /// <summary>
        /// 4 bytes
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int ToInt(this string str)
        {
            return Convert.ToInt32(str);
        }

        /// <summary>
        /// 8 bytes
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static long ToLong(this string str)
        {
            return Convert.ToInt64(str);
        }

        public static bool IsNullOrZeroEmpty(this string str)
        {
            if (string.IsNullOrEmpty(str) || str == "0") return true;

            return false;
        }

        /// <summary>
        /// HexRGB To Color
        /// </summary>
        /// <param name="hexColor">16 進制的 color #FFFFFF</param>
        /// <returns></returns>
        public static Color HexRGBToColor(this string hexColor)
        {
            if (ColorUtility.TryParseHtmlString(hexColor, out Color color)) return color;
            return Color.clear;
        }
        #endregion

        #region byte 擴充
        public static byte[] Slice(this byte[] source, int start, int end)
        {
            if (end < 0)
            {
                end = source.Length + end;
            }

            int len = end - start;
            byte[] sliceBytes = new byte[len];
            for (int i = 0; i < len; i++)
            {
                sliceBytes[i] = source[start + i];
            }

            return sliceBytes;
        }
        #endregion

        #region object 擴充
        public static byte ToByte(this object obj)
        {
            return Convert.ToByte(obj);
        }

        /// <summary>
        /// 2 bytes
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static short ToShort(this object obj)
        {
            return Convert.ToInt16(obj);
        }

        /// <summary>
        /// 4 bytes
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ToInt(this object obj)
        {
            return Convert.ToInt32(obj);
        }

        /// <summary>
        /// 8 bytes
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long ToLong(this object obj)
        {
            return Convert.ToInt64(obj);
        }

        public static bool ToBool(this object obj)
        {
            return Convert.ToBoolean(obj);
        }

        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.None);
        }

        public static string ToJsonWithIndented(this object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
        #endregion

        #region Int32 擴充
        public static byte ToByte(this int @int)
        {
            return Convert.ToByte(@int);
        }

        /// <summary>
        /// 2 bytes
        /// </summary>
        /// <param name="int"></param>
        /// <returns></returns>
        public static short ToShort(this int @int)
        {
            return Convert.ToInt16(@int);
        }

        #endregion

        #region UI Element 擴充

        /// <summary>
        /// ButtonPlus 事件註冊, 單擊事件
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="clickEvent"></param>
        /// <param name="soundAction"></param>
        public static void On(this ButtonPlus btn, UnityAction clickEvent, Action soundAction = null)
        {
            // 加入單點事件
            btn.onClick?.RemoveAllListeners();
            btn.onClick?.AddListener(clickEvent);
            btn.onClick?.AddListener(() => soundAction?.Invoke());
        }

        /// <summary>
        /// ButtonPlus 事件註冊, 單擊事件, 長按事件
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="clickEvent"></param>
        /// <param name="soundAction"></param>
        /// <param name="longClickPressedEvent"></param>
        /// <param name="longClickReleasedEvent"></param>
        /// <param name="triggerTime"></param>
        /// <param name="intervalTime"></param>
        public static void On(
            this ButtonPlus btn,
            UnityAction clickEvent,
            Action soundAction = null,
            UnityAction longClickPressedEvent = null,
            UnityAction longClickReleasedEvent = null,
            float triggerTime = 1f,
            float intervalTime = 0.1f)
        {
            // 配置長按設定
            btn.intervalTime = intervalTime;
            btn.triggerTime = triggerTime;

            // 加入單點事件
            btn.onClick?.RemoveAllListeners();
            btn.onClick?.AddListener(clickEvent);
            btn.onClick?.AddListener(() => soundAction?.Invoke());

            // 加入長按事件
            btn.onLongClickPressed?.RemoveAllListeners();
            btn.onLongClickPressed?.AddListener(longClickPressedEvent);
            btn.onLongClickPressed?.AddListener(() => soundAction?.Invoke());
            btn.onLongClickReleased?.RemoveAllListeners();
            btn.onLongClickReleased?.AddListener(longClickReleasedEvent);
        }

        /// <summary>
        /// Button 事件註冊, 單擊事件
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="clickEvent"></param>
        /// <param name="soundAction"></param>
        public static void On(this Button btn, UnityAction clickEvent, Action soundAction = null)
        {
            // 加入單點事件
            btn.onClick?.RemoveAllListeners();
            btn.onClick?.AddListener(clickEvent);
            btn.onClick?.AddListener(() => soundAction?.Invoke());
        }

        /// <summary>
        /// Toggle 事件註冊
        /// </summary>
        /// <param name="toggle"></param>
        /// <param name="toggleEvent"></param>
        /// <param name="soundAction"></param>
        public static void On(this Toggle toggle, UnityAction<bool> toggleEvent, Action soundAction = null)
        {
            toggle.onValueChanged.RemoveAllListeners();
            toggle.onValueChanged.AddListener(toggleEvent);
            toggle.onValueChanged.AddListener((isOn) => { if (isOn) soundAction?.Invoke(); });
        }

        /// <summary>
        /// ScrollRect 事件註冊
        /// </summary>
        /// <param name="scrollRect"></param>
        /// <param name="scrollEvent"></param>
        public static void On(this ScrollRect scrollRect, UnityAction<Vector2> scrollEvent)
        {
            scrollRect.onValueChanged.RemoveAllListeners();
            scrollRect.onValueChanged.AddListener(scrollEvent);
        }

        /// <summary>
        /// Slider 事件註冊
        /// </summary>
        /// <param name="slider"></param>
        /// <param name="sliderEvent"></param>
        public static void On(this Slider slider, UnityAction<float> sliderEvent)
        {
            slider?.onValueChanged.RemoveAllListeners();
            slider?.onValueChanged.AddListener(sliderEvent);
        }

        public static void ScrollToBottom(this ScrollRect scrollRect)
        {
            scrollRect.verticalNormalizedPosition = 0;
        }

        public static void ScrollToTop(this ScrollRect scrollRect)
        {
            scrollRect.verticalNormalizedPosition = 1;
        }

        public static void ScrollToTarget(this ScrollRect scrollRect, RectTransform targetChild)
        {
            if (targetChild == null) return;

            Canvas.ForceUpdateCanvases();
            Vector2 viewportLocalPosition = scrollRect.viewport.localPosition;
            Vector2 childLocalPosition = targetChild.localPosition;
            Vector2 result = new Vector2(
                0 - (viewportLocalPosition.x + childLocalPosition.x),
                0 - (viewportLocalPosition.y + childLocalPosition.y)
            );
            scrollRect.content.localPosition = result;
        }

        public static bool IsScrollToTop(this ScrollRect scrollRect, float topOffset = 0)
        {
            if (scrollRect.content.anchoredPosition.y <= 0 + topOffset)
            {
                // ScrollView 捲動到最上方
                return true;
            }

            return false;
        }

        public static bool IsScrollToBottom(this ScrollRect scrollRect, float bottomOffset = 0)
        {
            if (scrollRect.content.anchoredPosition.y + scrollRect.viewport.rect.height >= scrollRect.content.rect.height - bottomOffset)
            {
                // ScrollView 捲動到最下方
                return true;
            }

            return false;
        }

        /// <summary>
        /// Dropdown 事件註冊
        /// </summary>
        /// <param name="dropdown"></param>
        /// <param name="dropdownEvent"></param>
        public static void On(this Dropdown dropdown, UnityAction<int> dropdownEvent)
        {
            dropdown.onValueChanged.RemoveAllListeners();
            dropdown.onValueChanged.AddListener(dropdownEvent);
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

        #region GameObject 擴充
        public static void RefreshGameObject(this GameObject go)
        {
            go.SetActive(false);
            go.SetActive(true);
        }

        /// <summary>
        /// 透過 LayerName 指定 Layer (遞迴)
        /// </summary>
        /// <param name="go"></param>
        /// <param name="layerName"></param>
        public static void SetLayerRecursively(this GameObject go, string layerName)
        {
            int layer = LayerMask.NameToLayer(layerName);
            if (layer == -1) return;

            go.layer = layer;
            foreach (Transform child in go.transform)
            {
                child.gameObject.SetLayerRecursively(layerName);
            }
        }

        /// <summary>
        /// 設置 Tag
        /// </summary>
        /// <param name="tagName"></param>
        public static void SetTag(this GameObject go, string tagName)
        {
            go.tag = tagName;
        }

        /// <summary>
        /// Checks if a GameObject has been destroyed.
        /// </summary>
        /// <param name="gameObject">GameObject reference to check for destructiveness</param>
        /// <returns>If the game object has been marked as destroyed by UnityEngine</returns>
        public static bool IsDestroyed(this GameObject gameObject)
        {
            // UnityEngine overloads the == operator for the GameObject type
            // and returns null when the object has been destroyed, but 
            // actually the object is still there but has not been cleaned up yet
            // if we test both we can determine if the object has been destroyed.
            return gameObject == null && !ReferenceEquals(gameObject, null);
        }
        #endregion

        #region Animator 擴充
        public static void ResetAllTriggerParams(this Animator animator)
        {
            foreach (var parameter in animator.parameters)
            {
                if (parameter.type == AnimatorControllerParameterType.Trigger)
                {
                    animator.ResetTrigger(parameter.name);
                }
            }
        }

        public static void ResetBoolean(this Animator animator, string name)
        {
            animator.SetBool(name, false);
        }

        public static void ResetAllBooleanParams(this Animator animator)
        {
            foreach (AnimatorControllerParameter parameter in animator.parameters)
            {
                if (parameter.type == AnimatorControllerParameterType.Bool)
                {
                    animator.SetBool(parameter.name, false);
                }
            }
        }

        public static void ResetInteger(this Animator animator, string name)
        {
            animator.SetInteger(name, 0);
        }

        public static void ResetAllIntegerParams(this Animator animator)
        {
            foreach (AnimatorControllerParameter parameter in animator.parameters)
            {
                if (parameter.type == AnimatorControllerParameterType.Int)
                {
                    animator.SetInteger(parameter.name, 0);
                }
            }
        }

        public static void ResetFloat(this Animator animator, string name)
        {
            animator.SetFloat(name, 0);
        }

        public static void ResetAllFloatParams(this Animator animator)
        {
            foreach (AnimatorControllerParameter parameter in animator.parameters)
            {
                if (parameter.type == AnimatorControllerParameterType.Int)
                {
                    animator.SetFloat(parameter.name, 0);
                }
            }
        }

        public static bool ContainsParam(this Animator animator, string paramName)
        {
            foreach (AnimatorControllerParameter param in animator.parameters)
            {
                if (param.name == paramName) return true;
            }
            return false;
        }
        #endregion

        #region Canvas 擴充
        public static Vector3 WorldToCanvasPosition(this Canvas canvas, Vector3 worldPosition, Camera camera = null)
        {
            if (camera == null) camera = Camera.main;

            var viewportPosition = camera.WorldToViewportPoint(worldPosition);
            return canvas.ViewportToCanvasPosition(viewportPosition);
        }

        public static Vector3 ScreenToCanvasPosition(this Canvas canvas, Vector3 screenPosition)
        {
            var viewportPosition = new Vector3(screenPosition.x / Screen.width, screenPosition.y / Screen.height, 0);
            return canvas.ViewportToCanvasPosition(viewportPosition);
        }

        public static Vector3 ViewportToCanvasPosition(this Canvas canvas, Vector3 viewportPosition)
        {
            var centerBasedViewPortPosition = viewportPosition - new Vector3(0.5f, 0.5f, 0);
            var canvasRect = canvas.GetComponent<RectTransform>();
            var scale = canvasRect.sizeDelta;
            return Vector3.Scale(centerBasedViewPortPosition, scale);
        }
        #endregion

        #region Task 擴充
        public static void CancelWithDispose(this CancellationTokenSource cts)
        {
            if (cts == null) return;
            cts.Cancel();
            cts.Dispose();
        }
        #endregion

        #region Texture2D 擴充
        /// <summary>
        /// Create new sprite out of Texture
        /// </summary>
        public static Sprite AsSprite(this Texture2D texture, Vector2 pivot = default, float pixelPerUnit = 100, uint extrude = 0, SpriteMeshType meshType = SpriteMeshType.FullRect)
        {
            var rect = new Rect(0, 0, texture.width, texture.height);
            pivot = pivot != Vector2.zero ? pivot : new Vector2(0.5f, 0.5f);
            return Sprite.Create(texture, rect, pivot, pixelPerUnit, extrude, meshType);
        }
        #endregion

        #region EventSystem 擴充
        public static void On(this EventTrigger trigger, EventTriggerType eventType, System.Action<PointerEventData> listener)
        {
            // Register events
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = eventType;
            entry.callback.AddListener(data => listener.Invoke((PointerEventData)data));
            trigger.triggers.Add(entry);
        }
        public static void RemoveAll(this EventTrigger trigger)
        {
            // RemoveAllListener
            for (int i = 0; i < trigger.triggers.Count; i++)
                trigger.triggers[i].callback.RemoveAllListeners();
            trigger.triggers.Clear();
        }
        #endregion

        #region TMP 擴充
        public static void Break(this TMP_Text textMesh, int maxNum)
        {
            textMesh.text = textMesh.text.Replace("<br>", "");

            textMesh.ForceMeshUpdate();
            string parsedText = textMesh.GetParsedText();
            int length = parsedText.Length;

            if (length < maxNum || maxNum == 0) return;

            List<char> charInfo = textMesh.text.ToList();
            int movePos = 0;
            for (int i = 0; i < length; i++)
            {
                if (((i + 1) % maxNum) == 0)
                {
                    int textIdx = textMesh.textInfo.characterInfo[i].index + movePos;
                    charInfo.Insert(textIdx + 1, '>');
                    charInfo.Insert(textIdx + 1, 'r');
                    charInfo.Insert(textIdx + 1, 'b');
                    charInfo.Insert(textIdx + 1, '<');
                    movePos += 4;
                }
            }

            textMesh.text = new string(charInfo.ToArray());
        }
        #endregion

        #region Delegate 擴充
        public static bool TryInvoke(this Action action)
        {
            if (action == null)
                return false;

            try
            {
                action.Invoke();
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"TryInvoke - Exception caught: {ex.Message}");
                return false;
            }
        }
        #endregion
    }
}