using TMPro;
using UnityEngine;

namespace Main.UI.InputFields
{
    public class UsernameInputField : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_InputField _inputField;

        public bool Interactable
        {
            get => _inputField.interactable;
            set => _inputField.interactable = value;
        }

        public string Text => _inputField.text;

        #region MonoBehaviour

        private void OnValidate() => _inputField ??= GetComponent<TMP_InputField>();

        #endregion
    }
}