using System;
using FishNet.Object;
using Networking;
using UniRx;
using UnityEngine;
using Zenject;

namespace Main.Entities.Player
{
    public class DeathHandler : NetworkBehaviour
    {
        [Header("References")]
        [SerializeField] private Health.Health _health;

        private Battle _battle;

        [Inject]
        private void Constructor(Battle battle)
        {
            _battle = battle;
        }

        private IDisposable _subscription;

        #region Networking

        public override void OnStartClient()
        {
            base.OnStartClient();

            if (IsOwner == false)
            {
                enabled = false;
                _subscription?.Dispose();
            }
        }

        #endregion

        #region MonoBehaviour

        private void OnEnable() => _subscription = _health.Value.Where(x => x == 0).Subscribe(_ => OnDied());

        private void OnDisable() => _subscription?.Dispose();

        #endregion

        private void OnDied()
        {
            _subscription.Dispose();
            _battle.Leave();
        }
    }
}