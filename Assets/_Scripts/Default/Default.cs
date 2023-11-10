using UnityEngine;

[DisallowMultipleComponent]
public class Default : MonoBehaviour
{
    private void Awake()
    {
        this.gameObject.name = $"{nameof(Default)}";
        DontDestroyOnLoad(this);
    }
}
