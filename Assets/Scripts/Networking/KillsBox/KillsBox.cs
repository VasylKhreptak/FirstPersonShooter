using Data;
using FishNet;
using FishNet.Connection;
using FishNet.Transporting;
using Networking.Messaging.Core;
using UnityEngine;
using Zenject;

namespace Networking.KillsBox
{
    public class KillsBox : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private MessageBox _messageBox;

        private ClientsData _clientsData;

        [Inject]
        private void Constructor(ClientsData clientsData)
        {
            _clientsData = clientsData;
        }

        public bool Enabled
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        #region MonoBehaivour

        private void Awake() => Enabled = false;

        private void OnEnable()
        {
            InstanceFinder.ServerManager.RegisterBroadcast<KillData>(OnServerReceivedKillData);
            InstanceFinder.ClientManager.RegisterBroadcast<KillData>(OnClientReceivedKillData);
        }

        private void OnDisable()
        {
            InstanceFinder.ServerManager.UnregisterBroadcast<KillData>(OnServerReceivedKillData);
            InstanceFinder.ClientManager.UnregisterBroadcast<KillData>(OnClientReceivedKillData);

            _messageBox.Clear();
        }

        #endregion

        private void OnClientReceivedKillData(KillData killData, Channel _) => DrawKillData(killData);

        private void OnServerReceivedKillData(NetworkConnection connection, KillData killData, Channel _) =>
            InstanceFinder.ServerManager.Broadcast(killData);

        private void DrawKillData(KillData killData)
        {
            string message = $"{killData.KillerUsername} killed {killData.VictimUsername}";
            _messageBox.Send(message);
        }

        public void RegisterKill(KillData killData)
        {
            if (InstanceFinder.IsServerStarted)
                InstanceFinder.ServerManager.Broadcast(killData);
            else if (InstanceFinder.IsClientStarted)
                InstanceFinder.ClientManager.Broadcast(killData);
        }
    }
}