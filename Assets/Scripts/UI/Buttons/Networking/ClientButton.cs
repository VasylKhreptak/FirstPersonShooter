using System.Net;
using FishNet;
using FishNet.Discovery;
using FishNet.Transporting;
using UI.Buttons.Core;
using UI.Dropdowns;
using UI.InputFields;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Buttons.Networking
{
    public class ClientButton : BaseButton
    {
        [Header("References")]
        [SerializeField] private Image _indicator;

        [Header("Indicator Colors")]
        [SerializeField] private Color _enabledColor;
        [SerializeField] private Color _intermediateColor;
        [SerializeField] private Color _disabledColor;

        private ConnectionDropdown _connectionDropdown;
        private NetworkDiscovery _networkDiscovery;
        private AddressInputField _addressInputField;

        [Inject]
        private void Constructor(ConnectionDropdown connectionDropdown, NetworkDiscovery networkDiscovery,
            AddressInputField addressInputField)
        {
            _connectionDropdown = connectionDropdown;
            _networkDiscovery = networkDiscovery;
            _addressInputField = addressInputField;
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

            InstanceFinder.ClientManager.OnClientConnectionState += OnClientConnectionStateChanged;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (InstanceFinder.ClientManager == null)
                return;

            InstanceFinder.ClientManager.OnClientConnectionState -= OnClientConnectionStateChanged;
        }

        private void OnDestroy() => _networkDiscovery.StopSearching();

        #endregion

        protected override void OnClicked()
        {
            _isActive = !_isActive;

            if (_isActive)
            {
                TryConnect();

                return;
            }

            if (InstanceFinder.IsClientStarted == false)
                OnConnectionStateChanged(LocalConnectionState.Stopped);

            _networkDiscovery.StopSearching();
            InstanceFinder.ClientManager.StopConnection();
        }

        private void TryConnect()
        {
            StopSearchingServer();

            if (_connectionDropdown.Value == 0)
            {
                // InstanceFinder.ClientManager.StartConnection("localhost");
                OnConnectionStateChanged(LocalConnectionState.Starting);
                StartSearchingServer();
                return;
            }

            InstanceFinder.ClientManager.StartConnection(_addressInputField.Text,
                InstanceFinder.NetworkManager.TransportManager.Transport.GetPort());
        }

        private void OnClientConnectionStateChanged(ClientConnectionStateArgs statusArgs) =>
            OnConnectionStateChanged(statusArgs.ConnectionState);

        private void OnConnectionStateChanged(LocalConnectionState connectionState)
        {
            switch (connectionState)
            {
                case LocalConnectionState.Started:
                    _indicator.color = _enabledColor;
                    StopSearchingServer();
                    break;
                case LocalConnectionState.Stopped:
                    _indicator.color = _disabledColor;
                    _isActive = false;
                    StopSearchingServer();
                    break;
                case LocalConnectionState.Starting:
                case LocalConnectionState.Stopping:
                    _indicator.color = _intermediateColor;
                    break;
            }
        }

        private void StartSearchingServer()
        {
            StopSearchingServer();

            _networkDiscovery.SearchForServers();
            _networkDiscovery.ServerFoundCallback += OnServerFound;
        }

        private void StopSearchingServer()
        {
            _networkDiscovery.StopSearching();
            _networkDiscovery.ServerFoundCallback -= OnServerFound;
        }

        private void OnServerFound(IPEndPoint ipEndPoint)
        {
            StopSearchingServer();
            InstanceFinder.ClientManager.StartConnection(ipEndPoint.Address.ToString());
        }
    }
}