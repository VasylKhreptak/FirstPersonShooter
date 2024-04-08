using Main.Health;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace Utilities.DebugUtilities
{
    public class HealthDebug : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Health _health;

        [SerializeField] [ReadOnly] private float _healthValue;
        [SerializeField] [ReadOnly] private bool _isDeath;

        #region MonoBehaviour

        private void Awake()
        {
            _health.Value.Subscribe(value => _healthValue = value).AddTo(this);
            _health.IsDeath.Subscribe(value => _isDeath = value).AddTo(this);
        }

        #endregion

        [Button]
        private void Kill() => _health.TakeDamage(_health.Value.Value);

        [Button]
        private void Damage() => _health.TakeDamage(10f);
    }
}