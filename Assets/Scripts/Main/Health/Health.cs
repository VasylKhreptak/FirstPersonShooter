using FishNet.Object;
using FishNet.Object.Synchronizing;
using UniRx;
using UnityEngine;

namespace Main.Health
{
    public class Health : NetworkBehaviour, IDamageable
    {
        [Header("Preferences")]
        [SerializeField] private float _initialValue = 100f;
        [SerializeField] private float _maxValue = 100f;

        private readonly SyncVar<float> _syncVar = new SyncVar<float>();

        private readonly FloatReactiveProperty _value = new FloatReactiveProperty();

        public IReadOnlyReactiveProperty<float> Value => _value;

        public IReadOnlyReactiveProperty<float> Fill => _value.Select(value => value / _maxValue).ToReadOnlyReactiveProperty();

        public IReadOnlyReactiveProperty<bool> IsDeath => _value.Select(x => Mathf.Approximately(x, 0f)).ToReadOnlyReactiveProperty();

        #region MonoBehaviour

        private void Awake()
        {
            _value.Value = _initialValue;
            _syncVar.SetInitialValues(_initialValue);
            _syncVar.OnChange += OnHealthChanged;
        }

        private void OnDestroy()
        {
            _syncVar.OnChange -= OnHealthChanged;
        }

        #endregion

        private void OnHealthChanged(float previous, float value, bool isServer)
        {
            _value.Value = value;
        }

        public void TakeDamage(float damage) => TakeDamageServer(damage);

        [ServerRpc(RequireOwnership = false)]
        private void TakeDamageServer(float damage)
        {
            damage = Mathf.Clamp(damage, 0f, _syncVar.Value);
            _syncVar.Value -= damage;
        }
    }
}