using Data;
using FishNet.Object;
using Main.Health;
using UnityEngine;
using Zenject;

namespace Main.Entities.Player
{
    public class Player : NetworkBehaviour, IDamageable
    {
        [Header("References")]
        [SerializeField] private Health.Health _health;

        private ClientsData _clientsData;

        [Inject]
        private void Constructor(ClientsData clientsData)
        {
            _clientsData = clientsData;
        }

        #region MyRegion

        protected override void OnValidate()
        {
            base.OnValidate();

            _health ??= GetComponentInChildren<Health.Health>(true);
        }

        #endregion

        public void TakeDamage(float damage) => _health.TakeDamage(damage);

        public string GetUsername() => _clientsData.Get(Owner.ClientId).Username;
    }
}