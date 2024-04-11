using Data;
using FishNet;
using FishNet.Object;
using Infrastructure.Services.Log.Core;
using Main.Health.Damages;
using Networking;
using Serialization.MinMax;
using UI;
using UnityEngine;
using Visitor;
using Zenject;

namespace Main.Entities.Player
{
    public class PlayerShooter : NetworkBehaviour
    {
        [Header("References")]
        [SerializeField] private Camera _camera;
        [SerializeField] private NetworkAudio _fireAudio;

        [Header("Preferences")]
        [SerializeField] private float _interval = 0.07f;
        [SerializeField] private FloatMinMax _damage = new FloatMinMax(5f, 10f);

        private ILogService _logService;
        private HitIndicator _hitIndicator;
        private ClientsData _clientsData;

        [Inject]
        private void Constructor(ILogService logService, HitIndicator hitIndicator, ClientsData clientsData)
        {
            _logService = logService;
            _hitIndicator = hitIndicator;
            _clientsData = clientsData;
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
            _fireAudio.Play();

            Transform cameraTransform = _camera.transform;
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

            if (Physics.Raycast(ray, out RaycastHit hitInfo) == false)
                return;

            _logService.Log("Hit GameObject: " + hitInfo.collider.gameObject.name);

            if (hitInfo.collider.TryGetComponent(out IVisitable<PlayerDamage> visitable))
            {
                PlayerDamage playerDamage = new PlayerDamage(GetUsername(), _damage.Random());
                visitable.Accept(playerDamage);
                _hitIndicator.Trigger();
            }
        }

        private string GetUsername() => _clientsData.Get(InstanceFinder.ClientManager.Connection.ClientId).Username;
    }
}