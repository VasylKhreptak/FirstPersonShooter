using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons.Core
{
    public abstract class BaseButton : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] Button _button;

        protected Button Button => _button;

        #region MonoBehaviour

        private void OnValidate() => _button ??= GetComponent<Button>();

        private void OnEnable() => _button.onClick.AddListener(OnClicked);

        private void OnDisable() => _button.onClick.RemoveListener(OnClicked);

        #endregion

        protected abstract void OnClicked();
    }
}