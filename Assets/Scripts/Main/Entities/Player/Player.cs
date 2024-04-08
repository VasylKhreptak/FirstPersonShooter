using Main.Health;
using UnityEngine;

namespace Main.Entities.Player
{
    public class Player : MonoBehaviour, IDamageable
    {
        [Header("References")]
        [SerializeField] private Health.Health _health;

        #region MyRegion

        private void OnValidate() => _health ??= GetComponentInChildren<Health.Health>(true);

        #endregion

        public void TakeDamage(float damage) => _health.TakeDamage(damage);
    }
}