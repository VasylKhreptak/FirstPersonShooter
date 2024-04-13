using FishNet;
using FishNet.Discovery;
using FishNet.Transporting;
using UI.Buttons.Core;
using UI.Dropdowns;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Buttons.Networking
{
    public class ServerButton : BaseButton
    {
        [Header("References")]
        [SerializeField] private Image _indicator;

        [Header("Indicator Colors")]
        [SerializeField] private Color _enabledColor;
        [SerializeField] private Color _intermediateColor;
        [SerializeField] private Color _disabledColor;

        private ConnectionDropdown _connectionDropdown;
        private NetworkDiscovery _networkDiscovery;

        [Inject]
        private void Constructor(ConnectionDropdown connectionDropdown, NetworkDiscovery networkDiscovery)
        {
            _connectionDropdown = connectionDropdown;
            _networkDiscovery = networkDiscovery;
        }

        private bool _isActive;

        #region MonoBehaviour

        private void Awake()
        {
            OnConnectionStateChanged(LocalConnectionState.Stopped);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            if (InstanceFinder.ServerManager == null)
                return;

            InstanceFinder.ServerManager.OnServerConnectionState += OnServerConnectionStateChanged;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (InstanceFinder.ServerManager == null)
                return;

            InstanceFinder.ServerManager.OnServerConnectionState -= OnServerConnectionStateChanged;
        }

        private void OnDestroy() => _networkDiscovery.StopSearchingOrAdvertising();

        #endregion

        protected override void OnClicked()
        {
            _isActive = !_isActive;

            if (_isActive)
            {
                InstanceFinder.ServerManager.StartConnection();
                return;
            }

            _networkDiscovery.StopSearchingOrAdvertising();
            InstanceFinder.ServerManager.StopConnection(true);
        }

        private void OnServerConnectionStateChanged(ServerConnectionStateArgs statusArgs) =>
            OnConnectionStateChanged(statusArgs.ConnectionState);

        private void OnConnectionStateChanged(LocalConnectionState connectionState)
        {
            switch (connectionState)
            {
                case LocalConnectionState.Started:
                    _indicator.color = _enabledColor;
                    if (_connectionDropdown.Value == 0)
                        _networkDiscovery.AdvertiseServer();
                    break;
                case LocalConnectionState.Stopped:
                    _indicator.color = _disabledColor;
                    break;
                case LocalConnectionState.Starting:
                case LocalConnectionState.Stopping:
                    _indicator.color = _intermediateColor;
                    break;
            }
        }
    }
}