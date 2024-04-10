using FishNet.Broadcast;

namespace UI.Chat
{
    public struct Message : IBroadcast
    {
        public string Username;
        public string Content;
    }
}