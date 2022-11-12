using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointGizmo : MonoBehaviour
{
    enum DrawType
    {
        SOLID,
        WIRE
    }

    enum ColorSet
    {
        RED,
        YELLOW,
        GREEN,
        CYAN,
        BLUE
    }

    [SerializeReference] private ColorSet _colorSet = ColorSet.RED;
    [SerializeField] private DrawType _drawType = DrawType.SOLID;
    public float radius = 0.15f;

    private void OnDrawGizmos()
    {
        switch (this._colorSet)
        {
            case ColorSet.RED:
                Gizmos.color = Color.red;
                break;
            case ColorSet.YELLOW:
                Gizmos.color = Color.yellow;
                break;
            case ColorSet.GREEN:
                Gizmos.color = Color.green;
                break;
            case ColorSet.CYAN:
                Gizmos.color = Color.cyan;
                break;
            case ColorSet.BLUE:
                Gizmos.color = Color.blue;
                break;
        }

        switch (this._drawType)
        {
            case DrawType.SOLID:
                Gizmos.DrawSphere(this.transform.position, this.radius);
                break;
            case DrawType.WIRE:
                Gizmos.DrawWireSphere(this.transform.position, this.radius);
                break;
        }
    }
}
