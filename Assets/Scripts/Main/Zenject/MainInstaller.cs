using System.Collections.Generic;
using Graphics;
using Main.UI;
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
        [SerializeField] private BattleController _battleController;
        [SerializeField] private CursorLocker _cursorLocker;
        [SerializeField] private Touchpad _touchpad;
        [SerializeField] private ShootProcessor _shootProcessor;

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
        }

        #endregion

        public override void InstallBindings()
        {
            Container.Bind<ClientServerConnection>().AsSingle();
            Container.BindInstance(_crosshair).AsSingle();
            Container.BindInstance(_mapCamera).AsSingle();
            Container.Bind<PlayerSpawnPoints>().AsSingle().WithArguments(_playerSpawnPoints);
            Container.BindInstance(_battleController).AsSingle();
            Container.BindInstance(_cursorLocker).AsSingle();
            Container.BindInterfacesTo<CursorUnlocker>().AsSingle();
            Container.BindInstance(_touchpad).AsSingle();
            Container.BindInstance(_shootProcessor).AsSingle();
        }
    }
}