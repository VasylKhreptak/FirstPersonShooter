using System.Collections.Generic;
using Data;
using FishNet.Discovery;
using Graphics;
using Infrastructure.Services.Audio;
using Infrastructure.Services.ClientSideSpawn;
using Map;
using Networking;
using Networking.KillsBox;
using Networking.Messaging.Chat;
using UI;
using UI.Buttons;
using UI.Dropdowns;
using UI.InputFields;
using UI.InputFields.Username;
using UnityEngine;
using Zenject;

namespace Main.Zenject
{
    public class MainInstaller : MonoInstaller
    {
        [Header("References")]
        [SerializeField] private MapCamera _mapCamera;
        [SerializeField] private Crosshair _crosshair;
        [SerializeField] private Battle _battle;
        [SerializeField] private CursorLocker _cursorLocker;
        [SerializeField] private Touchpad _touchpad;
        [SerializeField] private ClientsData _clientsData;
        [SerializeField] private UsernameInputField _usernameInputField;
        [SerializeField] private Chat _chat;
        [SerializeField] private JoinBattleButton _joinBattleButton;
        [SerializeField] private LeaveBattleButton _leaveBattleButton;
        [SerializeField] private HitIndicator _hitIndicator;
        [SerializeField] private KillsBox _killsBox;
        [SerializeField] private ClientSideSpawnService _clientSideSpawnService;
        [SerializeField] private AudioService _audioService;
        [SerializeField] private HealthView _healthView;
        [SerializeField] private NetworkDiscovery _networkDiscovery;
        [SerializeField] private ConnectionDropdown _connectionDropdown;
        [SerializeField] private AddressInputField _addressInputField;
        [SerializeField] private PlayInformation _playInformation;

        [Header("Preferences")]
        [SerializeField] private List<Transform> _playerSpawnPoints;

        #region MonoBehaviour

        private void OnValidate()
        {
            _mapCamera ??= FindObjectOfType<MapCamera>(true);
            _crosshair ??= FindObjectOfType<Crosshair>(true);
            _cursorLocker ??= FindObjectOfType<CursorLocker>(true);
            _touchpad ??= FindObjectOfType<Touchpad>(true);
            _clientsData ??= FindObjectOfType<ClientsData>(true);
            _usernameInputField ??= FindObjectOfType<UsernameInputField>(true);
            _chat ??= FindObjectOfType<Chat>(true);
            _joinBattleButton ??= FindObjectOfType<JoinBattleButton>(true);
            _leaveBattleButton ??= FindObjectOfType<LeaveBattleButton>(true);
            _hitIndicator ??= FindObjectOfType<HitIndicator>(true);
            _killsBox ??= FindObjectOfType<KillsBox>(true);
            _clientSideSpawnService ??= FindObjectOfType<ClientSideSpawnService>(true);
            _audioService ??= FindObjectOfType<AudioService>(true);
            _healthView ??= FindObjectOfType<HealthView>(true);
            _networkDiscovery ??= FindObjectOfType<NetworkDiscovery>(true);
            _connectionDropdown ??= FindObjectOfType<ConnectionDropdown>(true);
            _addressInputField ??= FindObjectOfType<AddressInputField>(true);
            _playInformation ??= FindObjectOfType<PlayInformation>(true);
        }

        #endregion

        public override void InstallBindings()
        {
            Container.BindInstance(_crosshair).AsSingle();
            Container.BindInstance(_mapCamera).AsSingle();
            Container.Bind<PlayerSpawnPoints>().AsSingle().WithArguments(_playerSpawnPoints);
            Container.BindInstance(_battle).AsSingle();
            Container.BindInstance(_cursorLocker).AsSingle();
            Container.BindInterfacesTo<CursorUnlocker>().AsSingle();
            Container.BindInstance(_touchpad).AsSingle();
            Container.BindInstance(_clientsData).AsSingle();
            Container.BindInstance(_usernameInputField).AsSingle();
            Container.BindInstance(_chat).AsSingle();
            Container.BindInstance(_joinBattleButton).AsSingle();
            Container.BindInstance(_leaveBattleButton).AsSingle();
            Container.BindInstance(_hitIndicator).AsSingle();
            Container.BindInstance(_killsBox).AsSingle();
            Container.BindInstance(_clientSideSpawnService).AsSingle();
            Container.BindInstance(_audioService).AsSingle();
            Container.BindInstance(_healthView).AsSingle();
            Container.BindInstance(_networkDiscovery).AsSingle();
            Container.BindInstance(_connectionDropdown).AsSingle();
            Container.BindInstance(_addressInputField).AsSingle();
            Container.BindInstance(_playInformation).AsSingle();
        }
    }
}