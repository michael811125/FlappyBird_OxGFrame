using OxGKit.LoggingSystem;
using UnityEngine;

namespace FlappyBird.Hotfix.Runtime
{
    public class PipeDestroyer : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D collider)
        {
            Logging.Print<HLogger>($"PipeController Hit: {collider.gameObject.name}");

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
}