using UnityEngine;
using UnityEngine.EventSystems;

namespace Graphics
{
    public class CursorLocker : MonoBehaviour, IPointerDownHandler
    {
        public bool Enabled
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        #region MonoBehaviour

        private void Awake() => Enabled = false;

        #endregion

        public void OnPointerDown(PointerEventData eventData) => Cursor.lockState = CursorLockMode.Locked;
    }
}