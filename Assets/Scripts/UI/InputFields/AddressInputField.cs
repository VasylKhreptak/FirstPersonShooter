using TMPro;
using UI.Dropdowns;
using UnityEngine;

namespace UI.InputFields
{
    public class AddressInputField : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_InputField _inputField;

        private ConnectionDropdown _connectionDropdown;

        public string Text => _inputField.text;

        public bool Interactable
        {
            get => _inputField.interactable;
            set => _inputField.interactable = value;
        }

        public bool Enabled
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        #region MonoBehaviour

        private void OnValidate() => _inputField ??= GetComponent<TMP_InputField>();

        #endregion
    }
}