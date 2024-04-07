using FishNet.Object;
using UnityEngine;

namespace Main.Entities.Player
{
    public class PlayerMovement : NetworkBehaviour
    {
        [Header("References")]
        [SerializeField] private CharacterController _characterController;

        [Header("Preferences")]
        [SerializeField] private float _speed = 2f;
        [SerializeField] private float _jumpHeight = 1f;

        private Transform _transform;

        private Vector3 _velocity;

        private bool _isGrounded;

        #region MonoBehaviour

        private void Awake() => _transform = _characterController.transform;

        protected override void OnValidate()
        {
            base.OnValidate();

            _characterController ??= GetComponentInParent<CharacterController>(true);
        }

        private void Update()
        {
            UpdateIsGroundedValue();
            HandleGravity();
            HandleMovement();
            HandleJump();
            Move();
        }

        #endregion

        #region Networking

        public override void OnStartClient()
        {
            base.OnStartClient();

            enabled = IsOwner;
        }

        #endregion

        private void UpdateIsGroundedValue() => _isGrounded = _characterController.isGrounded;

        private void HandleGravity()
        {
            if (_isGrounded)
            {
                _velocity.y = -2f;
                return;
            }

            _velocity.y += Physics.gravity.y * Time.deltaTime;
        }

        private void HandleMovement()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 direction = _transform.right * horizontal + _transform.forward * vertical;
            direction = Vector3.ClampMagnitude(direction, 1f);
            Vector3 motion = direction * _speed;

            _velocity.x = motion.x;
            _velocity.z = motion.z;
        }

        private void HandleJump()
        {
            if (Input.GetButtonDown("Jump") && _isGrounded)
                _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y);
        }

        private void Move() => _characterController.Move(_velocity * Time.deltaTime);
    }
}