using FishNet;
using FishNet.Transporting;
using UI.Buttons.Core;
using UnityEngine;
using UnityEngine.UI;

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

        private bool _isActive;

        #region MonoBehaviour

        private void Awake()
        {
            UpdateIndicatorColor(LocalConnectionState.Stopped);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            if (InstanceFinder.ClientManager == null)
                return;

            InstanceFinder.ClientManager.OnClientConnectionState += OnClientConnectionStateChanged;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (InstanceFinder.ClientManager == null)
                return;

            InstanceFinder.ClientManager.OnClientConnectionState -= OnClientConnectionStateChanged;
        }

        #endregion

        protected override void OnClicked()
        {
            _isActive = !_isActive;

            if (_isActive)
            {
                InstanceFinder.ClientManager.StartConnection();
                return;
            }

            InstanceFinder.ClientManager.StopConnection();
        }

        private void OnClientConnectionStateChanged(ClientConnectionStateArgs statusArgs) =>
            UpdateIndicatorColor(statusArgs.ConnectionState);

        private void UpdateIndicatorColor(LocalConnectionState connectionState)
        {
            switch (connectionState)
            {
                case LocalConnectionState.Started:
                    _indicator.color = _enabledColor;
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