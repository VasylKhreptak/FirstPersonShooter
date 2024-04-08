using System.Collections.Generic;
using Data;
using Graphics;
using Main.UI;
using Main.UI.InputFields;
using Main.Weapons.Core;
using Map;
using Networking;
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
        [SerializeField] private ShootProcessor _shootProcessor;
        [SerializeField] private ClientsData _clientsData;
        [SerializeField] private UsernameInputField _usernameInputField;

        [Header("Preferences")]
        [SerializeField] private List<Transform> _playerSpawnPoints;

        #region MonoBehaviour

        private void OnValidate()
        {
            _mapCamera ??= FindObjectOfType<MapCamera>(true);
            _crosshair ??= FindObjectOfType<Crosshair>(true);
            _cursorLocker ??= FindObjectOfType<CursorLocker>(true);
            _touchpad ??= FindObjectOfType<Touchpad>(true);
            _shootProcessor ??= FindObjectOfType<ShootProcessor>(true);
            _clientsData ??= FindObjectOfType<ClientsData>(true);
            _usernameInputField ??= FindObjectOfType<UsernameInputField>(true);
        }

        #endregion

        public override void InstallBindings()
        {
            Container.Bind<ClientServerConnection>().AsSingle();
            Container.BindInstance(_crosshair).AsSingle();
            Container.BindInstance(_mapCamera).AsSingle();
            Container.Bind<PlayerSpawnPoints>().AsSingle().WithArguments(_playerSpawnPoints);
            Container.BindInstance(_battle).AsSingle();
            Container.BindInstance(_cursorLocker).AsSingle();
            Container.BindInterfacesTo<CursorUnlocker>().AsSingle();
            Container.BindInstance(_touchpad).AsSingle();
            Container.BindInstance(_shootProcessor).AsSingle();
            Container.BindInstance(_clientsData).AsSingle();
            Container.BindInstance(_usernameInputField).AsSingle();
        }
    }
}