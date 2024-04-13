using TMPro;
using UI.InputFields;
using UnityEngine;
using Zenject;

namespace UI.Dropdowns
{
    public class ConnectionDropdown : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_Dropdown _dropdown;

        private AddressInputField _addressInputField;

        [Inject]
        private void Constructor(AddressInputField addressInputField)
        {
            _addressInputField = addressInputField;
        }

        public int Value => _dropdown.value;

        public bool Interactable
        {
            get => _dropdown.interactable;
            set => _dropdown.interactable = value;
        }

        #region MonoBehaviour

        private void OnValidate() => _dropdown ??= GetComponent<TMP_Dropdown>();

        private void OnEnable()
        {
            OnValueChanged(_dropdown.value);
            _dropdown.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDisable() => _dropdown.onValueChanged.RemoveListener(OnValueChanged);

        #endregion

        private void OnValueChanged(int value) => _addressInputField.Enabled = value == 1;
    }
}