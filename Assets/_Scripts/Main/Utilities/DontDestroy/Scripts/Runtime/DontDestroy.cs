using UnityEngine;

namespace FlappyBird.Main.Runtime
{
    [DisallowMultipleComponent]
    public class DontDestroy : MonoBehaviour
    {
        [SerializeField]
        private string _runtimeName = nameof(DontDestroy);

        private void Awake()
        {
            this.gameObject.name = $"{this._runtimeName}";
            DontDestroyOnLoad(this);
        }
    }
}