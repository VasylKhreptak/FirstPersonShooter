using FishNet;
using FishNet.Connection;
using FishNet.Object;
using FishNet.Transporting;
using Infrastructure.Services.Log.Core;
using Zenject;

namespace Data
{
    public class ClientDataCleaner : NetworkBehaviour
    {
        private ClientsData _clientsData;

        [Inject]
        private void Constructor(ClientsData clientsData, ILogService logService)
        {
            _clientsData = clientsData;
        }

        public override void OnStartServer()
        {
            base.OnStartServer();

            InstanceFinder.ServerManager.OnRemoteConnectionState += OnRemoveConnectionStateChanged;
        }

        public override void OnStopServer()
        {
            base.OnStopServer();

            InstanceFinder.ServerManager.OnRemoteConnectionState -= OnRemoveConnectionStateChanged;

            _clientsData.Clear();
        }

        private void OnRemoveConnectionStateChanged(NetworkConnection connection, RemoteConnectionStateArgs stateArgs)
        {
            if (stateArgs.ConnectionState == RemoteConnectionState.Stopped)
                _clientsData.Remove(connection.ClientId);
        }
    }
}