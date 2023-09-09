using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectGizmo : MonoBehaviour
{
    public Color color = new Color32(0, 255, 255, 255);

    public Vector2 originPos = new Vector2(0f, 0f);
    public Vector2 rectSize = new Vector2(0f, 0f);

    private void OnDrawGizmos()
    {
        Gizmos.color = this.color;
        Gizmos.DrawLine(new Vector3(this.originPos.x, this.originPos.y, 0), new Vector3(this.originPos.x, this.rectSize.y, 0));  // A點 -> B點
        Gizmos.DrawLine(new Vector3(this.originPos.x, this.rectSize.y, 0), new Vector3(this.rectSize.x, this.rectSize.y, 0));    // B點 -> C點  
        Gizmos.DrawLine(new Vector3(this.rectSize.x, this.rectSize.y, 0), new Vector3(this.rectSize.x, this.originPos.y, 0));    // C點 -> D點
        Gizmos.DrawLine(new Vector3(this.rectSize.x, this.originPos.y, 0), new Vector3(this.originPos.x, this.originPos.y, 0));  // D點 -> A點
    }
}
