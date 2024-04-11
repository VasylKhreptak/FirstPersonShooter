using UnityEngine;
using UnityEngine.UI;

namespace UI.Toggles.Core
{
    public class SpriteToggle : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Toggle _toggle;
        [SerializeField] private Image _targetGraphics;

        [Header("Preferences")]
        [SerializeField] private Sprite _spriteOn;
        [SerializeField] private Sprite _spriteOff;

        #region MonoBehaviour

        private void OnValidate() => _toggle ??= GetComponent<Toggle>();

        private void OnEnable()
        {
            OnValueChanged(_toggle.isOn);
            _toggle.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDisable() => _toggle.onValueChanged.RemoveListener(OnValueChanged);

        #endregion

        private void OnValueChanged(bool isOn) => _targetGraphics.sprite = isOn ? _spriteOn : _spriteOff;
    }
}