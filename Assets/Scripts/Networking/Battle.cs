using Extensions;
using FishNet.Connection;
using FishNet.Object;
using Graphics;
using Infrastructure.Data.Static.Core;
using Infrastructure.Services.Log.Core;
using Infrastructure.Services.StaticData.Core;
using Main.UI;
using Map;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Networking
{
    public class Battle : NetworkBehaviour
    {
        private IStaticDataService _staticDataService;
        private Crosshair _crosshair;
        private PlayerSpawnPoints _spawnPoints;
        private MapCamera _mapCamera;
        private CursorLocker _cursorLocker;
        private ILogService _logService;

        [Inject]
        private void Constructor(IStaticDataService staticDataService, Crosshair crosshair, PlayerSpawnPoints spawnPoints,
            MapCamera mapCamera, CursorLocker cursorLocker, ILogService logService)
        {
            _staticDataService = staticDataService;
            _crosshair = crosshair;
            _spawnPoints = spawnPoints;
            _mapCamera = mapCamera;
            _cursorLocker = cursorLocker;
            _logService = logService;
        }

        private GameObject _player;

        private bool _joined = false;
        private bool _isConnectedToServer;

        #region Networking

        public override void OnStartClient()
        {
            base.OnStartClient();

            _isConnectedToServer = true;
        }

        public override void OnStopClient()
        {
            base.OnStopClient();

            _isConnectedToServer = false;
        }

        #endregion

        public void Join()
        {
            if (_joined || _isConnectedToServer == false)
                return;

            _crosshair.Enabled = true;
            _mapCamera.Enabled = false;
            _cursorLocker.Enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            SpawnPlayer();

            _logService.Log("Joined battle");

            _joined = true;
        }

        public void Leave()
        {
            if (_joined == false)
                return;

            _crosshair.Enabled = false;
            _mapCamera.Enabled = true;
            _cursorLocker.Enabled = false;
            Cursor.lockState = CursorLockMode.None;
            DespawnPlayer();

            _logService.Log("Left battle");

            _joined = false;
        }

        [ServerRpc(RequireOwnership = false)]
        private void SpawnPlayer(NetworkConnection connection = null)
        {
            GameObject playerPrefab = _staticDataService.Prefabs[Prefab.Player];

            Transform spawnPoint = _spawnPoints.Get().Random();

            GameObject player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);

            _player = player;

            Spawn(player, connection);
        }

        [ServerRpc(RequireOwnership = false)]
        private void DespawnPlayer()
        {
            Despawn(_player);

            _player = null;
        }
    }
}