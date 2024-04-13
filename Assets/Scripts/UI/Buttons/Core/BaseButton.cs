using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons.Core
{
    public abstract class BaseButton : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Button _button;

        protected Button Button => _button;

        #region MonoBehaviour

        protected virtual void OnValidate() => _button ??= GetComponent<Button>();

        protected virtual void OnEnable() => _button.onClick.AddListener(OnClicked);

        protected virtual void OnDisable() => _button.onClick.RemoveListener(OnClicked);

        #endregion

        protected abstract void OnClicked();
    }
}