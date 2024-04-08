using FishNet.Object;
using Main.Weapons.Core;
using Serialization.MinMax;
using UnityEngine;
using Zenject;

namespace Main.Entities.Player
{
    public class PlayerShooter : NetworkBehaviour
    {
        [Header("References")]
        [SerializeField] private Camera _camera;

        [Header("Preferences")]
        [SerializeField] private float _interval = 0.07f;
        [SerializeField] private FloatMinMax _damage = new FloatMinMax(5f, 10f);

        private ShootProcessor _shootProcessor;

        [Inject]
        private void Constructor(ShootProcessor shootProcessor)
        {
            _shootProcessor = shootProcessor;
        }

        private float _time;

        #region Networking

        public override void OnStartClient()
        {
            base.OnStartClient();

            enabled = IsOwner;
        }

        #endregion

        #region MonoBehaviour

        private void Update()
        {
            if (Cursor.lockState != CursorLockMode.Locked)
                return;

            _time += Time.deltaTime;

            if (Input.GetMouseButton(0) && _time >= _interval)
            {
                Shoot();
                _time = 0;
            }
        }

        #endregion

        private void Shoot()
        {
            Ray ray = _camera.ScreenPointToRay(GetScreenCenter());

            _shootProcessor.Shoot(ray, _damage.Random());
        }

        private Vector3 GetScreenCenter() => new Vector3(Screen.width / 2, Screen.height / 2);
    }
}