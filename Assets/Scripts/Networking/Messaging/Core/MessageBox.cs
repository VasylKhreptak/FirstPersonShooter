using Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Networking.Messaging.Core
{
    [RequireComponent(typeof(VerticalLayoutGroup))]
    public class MessageBox : MonoBehaviour
    {
        [Header("Preferences")]
        [SerializeField] private GameObject _textPrefab;
        [SerializeField] private int _maxMessages = 10;

        public void Send(string msg)
        {
            GameObject messageObject = Instantiate(_textPrefab, transform);
            TMP_Text tmp = messageObject.GetComponent<TMP_Text>();
            tmp.text = msg;
            messageObject.transform.SetAsLastSibling();

            if (transform.childCount > _maxMessages)
                Destroy(transform.GetChild(0).gameObject);
        }

        public void Clear()
        {
            Transform[] messages = transform.GetChildren();

            foreach (Transform message in messages)
                Destroy(message.gameObject);
        }
    }
}