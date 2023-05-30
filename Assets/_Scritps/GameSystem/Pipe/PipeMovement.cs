using UnityEngine;

public class PipeMovement : MonoBehaviour
{
    public float moveSpeed = 3f;

    private void Update()
    {
        if (CoreManager.IsGameStart())
        {
            float dt = CoreManager.deltaTime;

            // Continuosly move the obstacles to the left if the game hasn't ended
            this.transform.position = new Vector2(this.transform.position.x - dt * moveSpeed, this.transform.position.y);
        }
    }
}
