using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthView : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _fill;

        [Header("Preferences")]
        [SerializeField] private Gradient _healthGradient;

        public bool Enabled
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        #region MonoBehaviour

        private void Awake() => Enabled = false;

        private void OnValidate() => _slider ??= GetComponent<Slider>();

        #endregion

        public void SetHealthFill(float fill)
        {
            _slider.value = fill;
            _fill.color = _healthGradient.Evaluate(fill);
        }
    }
}