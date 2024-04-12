using Data;
using Extensions;
using FishNet;
using FishNet.Object;
using Infrastructure.Data.Static.Core;
using Infrastructure.Services.Audio;
using Infrastructure.Services.ClientSideSpawn;
using Infrastructure.Services.Log.Core;
using Main.Health.Damages;
using Serialization.MinMax;
using UI;
using UnityEngine;
using Visitor;
using Zenject;
using AudioType = Infrastructure.Data.Static.Core.AudioType;

namespace Main.Entities.Player
{
    public class PlayerShooter : NetworkBehaviour
    {
        [Header("References")]
        [SerializeField] private Camera _camera;

        [Header("Preferences")]
        [SerializeField] private Transform _bulletSpawnPoint;
        [SerializeField] private float _interval = 0.07f;
        [SerializeField] private float _maxAngleDeviation = 1f;
        [SerializeField] private LayerMask _bulletDecalLayerMask;
        [SerializeField] private FloatMinMax _damage = new FloatMinMax(5f, 10f);

        private ILogService _logService;
        private HitIndicator _hitIndicator;
        private ClientsData _clientsData;
        private ClientSideSpawnService _clientSideSpawnService;
        private AudioService _audioService;

        [Inject]
        private void Constructor(ILogService logService, HitIndicator hitIndicator, ClientsData clientsData,
            ClientSideSpawnService clientSideSpawnService, AudioService audioService)
        {
            _logService = logService;
            _hitIndicator = hitIndicator;
            _clientsData = clientsData;
            _clientSideSpawnService = clientSideSpawnService;
            _audioService = audioService;
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
            Ray ray = new Ray(_camera.transform.position, GetShootDirection());

            _audioService.Play(AudioType.RifleFire, _bulletSpawnPoint.position);

            if (Physics.Raycast(ray, out RaycastHit hitInfo) == false)
                return;

            if (_bulletDecalLayerMask.ContainsLayer(hitInfo.collider.gameObject.layer))
            {
                SpawnDecal(hitInfo);
                PlayHitSound(hitInfo);
            }

            _logService.Log("Hit GameObject: " + hitInfo.collider.gameObject.name);

            if (hitInfo.collider.TryGetComponent(out IVisitable<PlayerDamage> visitable))
            {
                PlayerDamage playerDamage = new PlayerDamage(GetUsername(), _damage.Random());
                visitable.Accept(playerDamage);
                _hitIndicator.Trigger();
            }
        }

        private string GetUsername() => _clientsData.Get(InstanceFinder.ClientManager.Connection.ClientId).Username;

        private void SpawnDecal(RaycastHit hitInfo)
        {
            Quaternion rotation = Quaternion.LookRotation(hitInfo.normal);
            _clientSideSpawnService.Spawn(Prefab.StoneImpactParticle, hitInfo.point + hitInfo.normal * 0.01f, rotation);
        }

        private Vector3 GetShootDirection()
        {
            Transform cameraTransform = _camera.transform;

            Vector3 direction = cameraTransform.forward;

            float GetRandomAngle() => Random.Range(-_maxAngleDeviation, _maxAngleDeviation);

            direction = Quaternion.AngleAxis(GetRandomAngle(), cameraTransform.right) * direction;
            direction = Quaternion.AngleAxis(GetRandomAngle(), cameraTransform.up) * direction;

            return direction;
        }

        private void PlayHitSound(RaycastHit hitInfo) => _audioService.Play(AudioType.ConcreteImpact, hitInfo.point);
    }
}