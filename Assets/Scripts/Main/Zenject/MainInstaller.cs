using System.Collections.Generic;
using Data;
using Graphics;
using Map;
using Networking;
using Networking.Messaging.Chat;
using UI;
using UI.Buttons;
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
        }
    }
}