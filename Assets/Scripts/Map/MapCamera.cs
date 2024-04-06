using UnityEngine;

namespace Map
{
    public class MapCamera : MonoBehaviour
    {
        [Header("Preferences")]
        [SerializeField] private float _rotateSpeed = 5f;
        [SerializeField] private Vector3 _axis = Vector3.up;

        public bool Enabled
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        #region MonoBehaviour

        private void Update() => RotateCameraStep();

        #endregion

        private void RotateCameraStep() => transform.rotation *= Quaternion.AngleAxis(_rotateSpeed * Time.deltaTime, _axis);
    }
}