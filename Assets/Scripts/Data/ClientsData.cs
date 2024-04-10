using FishNet;
using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using FishNet.Transporting;
using Infrastructure.Services.Log.Core;
using Zenject;

namespace Data
{
    public class ClientsData : NetworkBehaviour
    {
        private readonly SyncDictionary<NetworkConnection, ClientData> Map = new SyncDictionary<NetworkConnection, ClientData>();

        private ILogService _logService;

        [Inject]
        private void Constructor(ILogService logService)
        {
            _logService = logService;
        }

        public override void OnStartServer()
        {
            base.OnStartServer();

            InstanceFinder.ServerManager.OnRemoteConnectionState += OnRemoteConnectionStateChanged;
        }

        public override void OnStopServer()
        {
            base.OnStopServer();

            InstanceFinder.ServerManager.OnRemoteConnectionState -= OnRemoteConnectionStateChanged;
            Map.Clear();
        }

        private void OnRemoteConnectionStateChanged(NetworkConnection connection, RemoteConnectionStateArgs state)
        {
            if (state.ConnectionState == RemoteConnectionState.Started)
            {
                _logService.Log("Client connected");
                Map.Add(connection, new ClientData());
                _logService.Log("Client data created");
            }
            else if (state.ConnectionState == RemoteConnectionState.Stopped)
            {
                _logService.Log("Client disconnected");
                Map.Remove(connection);
                _logService.Log("Client data removed");
            }
        }

        public ClientData Get(NetworkConnection connection) => Map[connection];

        public void Dirty(NetworkConnection connection) => Map.Dirty(connection);
    }
}