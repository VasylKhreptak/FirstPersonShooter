using UnityEngine;
using UnityEngine.UI;

namespace UI.Toggles
{
    public class AudioToggle : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Toggle _toggle;

        #region MonoBehaviour

        private void OnValidate() => _toggle ??= GetComponent<Toggle>();

        private void OnEnable()
        {
            ToggleAudio(_toggle.isOn);
            _toggle.onValueChanged.AddListener(ToggleAudio);
        }

        private void OnDisable() => _toggle.onValueChanged.RemoveListener(ToggleAudio);

        #endregion

        private void ToggleAudio(bool isOn) => AudioListener.volume = isOn ? 1 : 0;
    }
}