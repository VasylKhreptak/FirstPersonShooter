using UnityEngine;

namespace UI
{
    public class PlayInformation : MonoBehaviour
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