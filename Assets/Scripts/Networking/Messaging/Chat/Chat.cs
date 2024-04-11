using Data;
using FishNet;
using FishNet.Connection;
using FishNet.Transporting;
using Networking.Messaging.Core;
using UnityEngine;
using Zenject;

namespace Networking.Messaging.Chat
{
    public class Chat : MonoBehaviour
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
            InstanceFinder.ServerManager.RegisterBroadcast<Message>(OnServerReceivedMessage);
            InstanceFinder.ClientManager.RegisterBroadcast<Message>(OnClientReceivedMessage);
        }

        private void OnDisable()
        {
            InstanceFinder.ServerManager.UnregisterBroadcast<Message>(OnServerReceivedMessage);
            InstanceFinder.ClientManager.UnregisterBroadcast<Message>(OnClientReceivedMessage);

            _messageBox.Clear();
        }

        #endregion

        private void OnClientReceivedMessage(Message message, Channel _) => DrawMessage(message);

        private void OnServerReceivedMessage(NetworkConnection connection, Message message, Channel _) =>
            InstanceFinder.ServerManager.Broadcast(message);

        private void DrawMessage(Message message)
        {
            string content = $"{message.Username}: {message.Content}";
            _messageBox.Send(content);
        }

        public void SendMessage(string content)
        {
            Message message = new Message
            {
                Username = GetUsername(), Content = content
            };

            if (InstanceFinder.IsServerStarted)
                InstanceFinder.ServerManager.Broadcast(message);
            else if (InstanceFinder.IsClientStarted)
                InstanceFinder.ClientManager.Broadcast(message);
        }

        private string GetUsername() => _clientsData.Get(InstanceFinder.ClientManager.Connection.ClientId).Username;
    }
}