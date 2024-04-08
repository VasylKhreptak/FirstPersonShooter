using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;

namespace Data
{
    public class ClientsData : NetworkBehaviour
    {
        public readonly SyncDictionary<NetworkConnection, ClientData>
            ClientsMap = new SyncDictionary<NetworkConnection, ClientData>();
    }
}