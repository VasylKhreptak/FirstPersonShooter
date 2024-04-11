using Networking.Messaging.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Utilities.DebugUtilities
{
    public class MessageBoxDebug : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private MessageBox _messageBox;

        #region MonoBehaviour

        private void OnValidate() => _messageBox ??= GetComponent<MessageBox>();

        #endregion

        [Button]
        private void SendTestMessage() => _messageBox.Send("Message №" + Random.Range(0, 1000));
    }
}