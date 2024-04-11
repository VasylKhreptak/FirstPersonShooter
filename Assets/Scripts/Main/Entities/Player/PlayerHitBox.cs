using FishNet.Object;
using FishNet.Object.Synchronizing;
using Main.Health.Damages;
using UnityEngine;
using Visitor;

namespace Main.Entities.Player
{
    public class PlayerHitBox : NetworkBehaviour, IVisitable<PlayerDamage>
    {
        [Header("References")]
        [SerializeField] private Health.Health _health;

        private readonly SyncVar<PlayerDamage> _lastPlayerDamage = new SyncVar<PlayerDamage>();

        public PlayerDamage LastPlayerDamage => _lastPlayerDamage.Value;

        [ServerRpc(RequireOwnership = false)]
        public void Accept(PlayerDamage playerDamage)
        {
            _lastPlayerDamage.Value = playerDamage;
            _health.TakeDamage(playerDamage.Value);
        }
    }
}