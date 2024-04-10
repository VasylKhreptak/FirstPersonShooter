using UnityEngine;

namespace UI
{
    public class Crosshair : MonoBehaviour
    {
        public bool Enabled
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        #region MonoBehaviour

        private void Awake() => Enabled = false;

        #endregion
    }
}