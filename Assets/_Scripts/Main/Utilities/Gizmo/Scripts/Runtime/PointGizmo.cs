using UnityEngine;

namespace FlappyBird.Main.Runtime
{
    public class PointGizmo : MonoBehaviour
    {
        enum DrawType
        {
            Solid,
            Wire
        }

        enum ColorSet
        {
            Red,
            Yellow,
            Green,
            Cyan,
            Blue
        }

        [SerializeReference] private ColorSet _colorSet = ColorSet.Red;
        [SerializeField] private DrawType _drawType = DrawType.Solid;
        public float radius = 0.15f;

        private void OnDrawGizmos()
        {
            switch (this._colorSet)
            {
                case ColorSet.Red:
                    Gizmos.color = Color.red;
                    break;
                case ColorSet.Yellow:
                    Gizmos.color = Color.yellow;
                    break;
                case ColorSet.Green:
                    Gizmos.color = Color.green;
                    break;
                case ColorSet.Cyan:
                    Gizmos.color = Color.cyan;
                    break;
                case ColorSet.Blue:
                    Gizmos.color = Color.blue;
                    break;
            }

            switch (this._drawType)
            {
                case DrawType.Solid:
                    Gizmos.DrawSphere(this.transform.position, this.radius);
                    break;
                case DrawType.Wire:
                    Gizmos.DrawWireSphere(this.transform.position, this.radius);
                    break;
            }
        }
    }
}