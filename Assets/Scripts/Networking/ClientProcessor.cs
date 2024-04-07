using Extensions;
using FishNet.Connection;
using FishNet.Object;
using Infrastructure.Data.Static.Core;
using Infrastructure.Services.StaticData.Core;
using Map;
using UnityEngine;
using Zenject;

namespace Networking
{
    public class ClientProcessor : NetworkBehaviour
    {
        private MapCamera _mapCamera;
        private PlayerSpawnPoints _playerSpawnPoints;
        private IStaticDataService _staticDataService;

        [Inject]
        private void Constructor(MapCamera mapCamera, PlayerSpawnPoints playerSpawnPoints, IStaticDataService staticDataService)
        {
            _mapCamera = mapCamera;
            _playerSpawnPoints = playerSpawnPoints;
            _staticDataService = staticDataService;
        }

        private GameObject _player;

        public override void OnStartClient()
        {
            base.OnStartClient();

            StartClient();
        }

        public override void OnStopClient()
        {
            base.OnStopClient();

            StopClient();
        }

        private void StartClient()
        {
            _mapCamera.Enabled = false;
            SpawnPlayer();
        }

        private void StopClient()
        {
            _mapCamera.Enabled = true;
            DespawnPlayer();
        }

        [ServerRpc(RequireOwnership = false)]
        private void SpawnPlayer(NetworkConnection connection = null)
        {
            GameObject playerPrefab = _staticDataService.Prefabs[Prefab.Player];
            Transform spawnPoint = _playerSpawnPoints.Get().Random();

            _player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);

            Spawn(_player, connection);
        }

        [ServerRpc(RequireOwnership = false)]
        private void DespawnPlayer()
        {
            Debug.LogWarning(_player == null);
            Despawn(_player);
        }
    }
}