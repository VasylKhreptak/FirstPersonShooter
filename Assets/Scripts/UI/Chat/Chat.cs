using Data;
using Extensions;
using FishNet;
using FishNet.Connection;
using FishNet.Transporting;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI.Chat
{
    public class Chat : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform _messageBoxTransform;
        [SerializeField] private TMP_InputField _inputField;

        [Header("Preferences")]
        [SerializeField] private GameObject _messageTextPrefab;
        [SerializeField] private int _maxMessages = 20;

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
            _inputField.onSubmit.AddListener(OnInputFieldSubmitted);

            InstanceFinder.ServerManager.RegisterBroadcast<Message>(OnServerReceivedMessage);
            InstanceFinder.ClientManager.RegisterBroadcast<Message>(OnClientReceivedMessage);
        }

        private void OnDisable()
        {
            _inputField.onSubmit.RemoveListener(OnInputFieldSubmitted);

            InstanceFinder.ServerManager.UnregisterBroadcast<Message>(OnServerReceivedMessage);
            InstanceFinder.ClientManager.UnregisterBroadcast<Message>(OnClientReceivedMessage);

            ClearMessages();
        }

        #endregion

        private void OnClientReceivedMessage(Message message, Channel _) => DrawMessage(message);

        private void OnServerReceivedMessage(NetworkConnection connection, Message message, Channel channel) =>
            InstanceFinder.ServerManager.Broadcast(message);

        private void DrawMessage(Message message)
        {
            GameObject messageText = Instantiate(_messageTextPrefab, _messageBoxTransform);
            TMP_Text tmp = messageText.GetComponent<TMP_Text>();
            tmp.text = $"{message.Username}: {message.Content}";
            messageText.transform.SetAsFirstSibling();

            if (_messageBoxTransform.childCount > _maxMessages)
                Destroy(_messageBoxTransform.GetChild(_messageBoxTransform.childCount - 1).gameObject);
        }

        private void ClearMessages()
        {
            Transform[] messages = _messageBoxTransform.GetChildren();

            foreach (Transform message in messages)
            {
                Destroy(message.gameObject);
            }
        }

        private void SendMessage(Message message)
        {
            if (InstanceFinder.IsServerStarted)
                InstanceFinder.ServerManager.Broadcast(message);
            else if (InstanceFinder.IsClientStarted)
                InstanceFinder.ClientManager.Broadcast(message);
        }

        private void OnInputFieldSubmitted(string text)
        {
            if (IsMessageValid(text) == false)
                return;

            Message message = new Message
            {
                Username = GetClientUsername(), Content = text
            };

            _inputField.text = string.Empty;
            SendMessage(message);
        }

        private string GetClientUsername() => _clientsData.Get(InstanceFinder.ClientManager.Connection.ClientId).Username;

        private bool IsMessageValid(string message) => string.IsNullOrWhiteSpace(message) == false;
    }
}