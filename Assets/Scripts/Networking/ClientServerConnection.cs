using FishNet;

namespace Networking
{
    public class ClientServerConnection
    {
        public void Start(bool isServer)
        {
            if (isServer)
                InstanceFinder.ServerManager.StartConnection();

            InstanceFinder.ClientManager.StartConnection();
        }
    }
}