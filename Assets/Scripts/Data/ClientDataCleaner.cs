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
        private ILogService _logService;

        [Inject]
        private void Constructor(ClientsData clientsData, ILogService logService)
        {
            _clientsData = clientsData;
            _logService = logService;
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

            _logService.Log("Server stopped. All client data removed");
        }

        private void OnRemoveConnectionStateChanged(NetworkConnection connection, RemoteConnectionStateArgs stateArgs)
        {
            if (stateArgs.ConnectionState == RemoteConnectionState.Stopped)
            {
                _logService.Log("Client disconnected");
                _clientsData.Remove(connection.ClientId);
                _logService.Log("Client data removed");
            }
        }
    }
}