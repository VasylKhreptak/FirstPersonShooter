using UnityEngine;
using UnityEngine.EventSystems;

namespace Main.UI
{
    public class Touchpad : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private UIBehaviour _behaviour;

        public UIBehaviour Behaviour => _behaviour;

        #region MonoBehaviour

        private void OnValidate() => _behaviour ??= GetComponent<UIBehaviour>();

        #endregion
    }
}