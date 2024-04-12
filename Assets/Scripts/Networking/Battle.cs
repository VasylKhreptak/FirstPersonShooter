using System.Collections.Generic;
using Data;
using Extensions;
using FishNet;
using FishNet.Connection;
using FishNet.Object;
using Graphics;
using Infrastructure.Data.Static.Core;
using Infrastructure.Services.Log.Core;
using Infrastructure.Services.StaticData.Core;
using Map;
using Networking.Messaging.Chat;
using UI;
using UI.Buttons;
using UnityEngine;
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
        private JoinBattleButton _joinBattleButton;
        private LeaveBattleButton _leaveBattleButton;
        private Chat _chat;
        private ClientsData _clientsData;

        [Inject]
        private void Constructor(IStaticDataService staticDataService, Crosshair crosshair, PlayerSpawnPoints spawnPoints,
            MapCamera mapCamera, CursorLocker cursorLocker, ILogService logService, JoinBattleButton joinBattleButton,
            LeaveBattleButton leaveBattleButton, Chat chat, ClientsData clientsData)
        {
            _staticDataService = staticDataService;
            _crosshair = crosshair;
            _spawnPoints = spawnPoints;
            _mapCamera = mapCamera;
            _cursorLocker = cursorLocker;
            _logService = logService;
            _joinBattleButton = joinBattleButton;
            _leaveBattleButton = leaveBattleButton;
            _chat = chat;
            _clientsData = clientsData;
        }

        private bool _joined;
        private bool _isConnectedToServer;

        private readonly Dictionary<int, GameObject> _idPlayerObjectMap = new Dictionary<int, GameObject>();

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
            if (_joined ||
                _isConnectedToServer == false ||
                _clientsData.HasKey(InstanceFinder.ClientManager.Connection.ClientId) == false)
                return;

            _crosshair.Enabled = true;
            _mapCamera.Enabled = false;
            _cursorLocker.Enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            _joinBattleButton.Interactable = false;
            _leaveBattleButton.Interactable = true;
            _chat.SendMessage("joined the battle");
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
            _joinBattleButton.Interactable = true;
            _leaveBattleButton.Interactable = false;
            _chat.SendMessage("left the battle");
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

            _idPlayerObjectMap.Add(connection.ClientId, player);

            Spawn(player, connection);
        }

        [ServerRpc(RequireOwnership = false)]
        private void DespawnPlayer(NetworkConnection connection = null)
        {
            GameObject playerObject = _idPlayerObjectMap[connection.ClientId];

            _idPlayerObjectMap.Remove(connection.ClientId);

            Despawn(playerObject);
        }
    }
}