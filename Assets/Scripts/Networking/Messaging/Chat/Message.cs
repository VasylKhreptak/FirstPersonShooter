using FishNet.Broadcast;

namespace Networking.Messaging.Chat
{
    public struct Message : IBroadcast
    {
        public string Username;
        public string Content;
    }
}