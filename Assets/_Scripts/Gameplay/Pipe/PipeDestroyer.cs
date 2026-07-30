using OxGKit.LoggingSystem;
using UnityEngine;

/// <summary>
/// A trigger off the left edge of the screen that recycles pipes once they pass by.
/// <para>
/// 置於畫面左側邊界外的觸發器, 水管通過後即回收銷毀。
/// </para>
/// </summary>
public class PipeDestroyer : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        Logging.Print<MLogger>($"PipeController Hit: {collider.gameObject.name}");

        if (collider.gameObject.transform.parent != null)
        {
            Destroy(collider.gameObject.transform.parent.gameObject);
        }
        else
        {
            Destroy(collider.gameObject);
        }
    }
}
