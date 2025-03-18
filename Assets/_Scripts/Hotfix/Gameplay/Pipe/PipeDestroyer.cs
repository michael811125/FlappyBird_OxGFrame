using UnityEngine;

namespace FlappyBird.Hotfix.Runtime
{
    public class PipeDestroyer : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D collider)
        {
            Debug.Log($"PipeController Hit: {collider.gameObject.name}");

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