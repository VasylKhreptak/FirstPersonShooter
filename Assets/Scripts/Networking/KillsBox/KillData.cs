using FishNet.Broadcast;

namespace Networking.KillsBox
{
    public struct KillData : IBroadcast
    {
        public string KillerUsername;
        public string VictimUsername;
    }
}