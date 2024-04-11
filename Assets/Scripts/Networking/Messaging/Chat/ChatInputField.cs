using TMPro;
using UnityEngine;
using Zenject;

namespace Networking.Messaging.Chat
{
    public class ChatInputField : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_InputField _inputField;

        private Chat _chat;

        [Inject]
        private void Constructor(Chat chat)
        {
            _chat = chat;
        }

        #region MonoBehaviour

        private void OnValidate() => _inputField ??= GetComponent<TMP_InputField>();

        private void OnEnable() => _inputField.onSubmit.AddListener(OnSubmit);

        private void OnDisable() => _inputField.onSubmit.RemoveListener(OnSubmit);

        #endregion

        private void OnSubmit(string msg)
        {
            if (IsMessageValid(msg) == false)
                return;

            _chat.SendMessage(msg);

            _inputField.text = string.Empty;
        }

        private bool IsMessageValid(string msg) => string.IsNullOrWhiteSpace(msg) == false;
    }
}