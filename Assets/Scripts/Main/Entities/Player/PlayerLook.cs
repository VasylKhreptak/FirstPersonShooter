using Extensions;
using FishNet.Object;
using UnityEngine;

namespace Main.Entities.Player
{
    public class PlayerLook : NetworkBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform _body;
        [SerializeField] private Transform _head;

        [Header("Preferences")]
        [SerializeField] private float _sensitivity = 3f;
        [SerializeField] private float _minAngle = -85f;
        [SerializeField] private float _maxAngle = 85f;
        [SerializeField] private bool _inverse = true;

        private float _mouseX;
        private float _mouseY;
        private float _xRotation;

        #region MonoBehaviour

        private void Update() => LookStep();

        #endregion

        #region Networking

        public override void OnStartClient()
        {
            base.OnStartClient();

            enabled = IsOwner;
        }

        #endregion

        private void LookStep()
        {
            if (Cursor.lockState != CursorLockMode.Locked)
                return;

            _mouseX = Input.GetAxis("Mouse X") * _sensitivity;
            _mouseY = Input.GetAxis("Mouse Y") * _sensitivity * _inverse.Sign();

            _body.Rotate(Vector3.up * _mouseX);

            _xRotation = _head.localEulerAngles.x - _mouseY;
            _xRotation = _xRotation > 180 ? _xRotation - 360 : _xRotation;
            _xRotation = Mathf.Clamp(_xRotation, _minAngle, _maxAngle);

            _head.localEulerAngles = new Vector3(_xRotation, 0, 0);
        }
    }
}