using UnityEngine;

namespace FlappyBird.Hotfix.Runtime
{
    /// <summary>
    /// Scrolls a pipe from right to left while the gameplay is running.
    /// <para>
    /// 遊戲進行中讓水管由右往左捲動。
    /// </para>
    /// </summary>
    public class PipeMovement : MonoBehaviour
    {
        public float moveSpeed = 3f;

        private void Update()
        {
            if (HCoreSystem.IsGameStart())
            {
                float dt = HCoreSystem.deltaTime;

                // Continuosly move the obstacles to the left if the game hasn't ended
                // 遊戲尚未結束時持續將障礙物往左移動
                this.transform.position = new Vector2(this.transform.position.x - dt * moveSpeed, this.transform.position.y);
            }
        }
    }
}