using System.Collections.Generic;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using Infrastructure.Services.Log.Core;
using Zenject;

namespace Data
{
    public class ClientsData : NetworkBehaviour
    {
        private ILogService _logService;

        [Inject]
        private void Constructor(ILogService logService)
        {
            _logService = logService;
        }

        private readonly SyncDictionary<int, ClientData> _map = new SyncDictionary<int, ClientData>();

        public ClientData Get(int id) => _map[id];

        public void Add(int id, ClientData clientData) => AddServer(id, clientData);

        public void Remove(int id) => RemoveServer(id);

        public void Clear() => ClearServer();

        public bool HasKey(int id) => _map.ContainsKey(id);

        [ServerRpc(RequireOwnership = false)]
        private void AddServer(int id, ClientData clientData)
        {
            _map.TryAdd(id, clientData);
            _logService.Log($"Client with username {clientData.Username} added to ClientsData");
        }

        [ServerRpc(RequireOwnership = false)]
        private void RemoveServer(int id)
        {
            if (_map.TryGetValue(id, out ClientData clientData))
                _logService.Log($"Client with username {clientData.Username} removed from ClientsData");

            _map.Remove(id);
        }

        [ServerRpc(RequireOwnership = false)]
        private void ClearServer()
        {
            _map.Clear();
            _logService.Log("ClientsData cleared");
        }
    }
}