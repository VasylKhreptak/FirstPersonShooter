using System.Collections.Generic;
using FishNet.Object;
using FishNet.Object.Synchronizing;

namespace Data
{
    public class ClientsData : NetworkBehaviour
    {
        private readonly SyncDictionary<int, ClientData> _map = new SyncDictionary<int, ClientData>();

        public ClientData Get(int id) => _map[id];

        public void Add(int id, ClientData clientData) => AddServer(id, clientData);

        public void Remove(int id) => RemoveServer(id);

        public void Clear() => ClearServer();

        public bool HasKey(int id) => _map.ContainsKey(id);

        [ServerRpc(RequireOwnership = false)]
        private void AddServer(int id, ClientData clientData) => _map.Add(id, clientData);

        [ServerRpc(RequireOwnership = false)]
        private void RemoveServer(int id) => _map.Remove(id);

        [ServerRpc(RequireOwnership = false)]
        private void ClearServer() => _map.Clear();
    }
}