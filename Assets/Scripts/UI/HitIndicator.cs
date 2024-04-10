using System;
using UniRx;
using UnityEngine;

namespace UI
{
    public class HitIndicator : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject _iconGameObject;

        [Header("Preferences")]
        [SerializeField] private float _showTime = 0.5f;

        private IDisposable _subscription;

        #region MonoBehaviour

        private void Awake() => Hide();

        private void OnDisable() => Hide();

        #endregion

        public void Trigger()
        {
            _subscription?.Dispose();
            _iconGameObject.SetActive(true);
            _subscription = Observable.Timer(TimeSpan.FromSeconds(_showTime)).Subscribe(_ => Hide());
        }

        public void Hide()
        {
            _subscription?.Dispose();
            _iconGameObject.SetActive(false);
        }
    }
}