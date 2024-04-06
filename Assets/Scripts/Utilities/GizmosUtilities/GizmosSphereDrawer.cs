using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Utilities.GizmosUtilities
{
    public class GizmosSphereDrawer : MonoBehaviour
    {
        [Header("Preferences")]
        [SerializeField] private Color _color = Color.red.WithAlpha(0.5f);
        [SerializeField] [Min(0)] private float _range = 0.2f;
        [SerializeField] private bool _drawOnlyWhenSelected = true;

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            if (_drawOnlyWhenSelected && Selection.Contains(gameObject) == false)
                return;

            Gizmos.color = _color;
            Gizmos.DrawSphere(transform.position, _range);
        }

#endif
    }
}