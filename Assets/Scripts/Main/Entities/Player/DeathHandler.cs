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

        #region MonoBehaviour

        public override void OnStartClient()
        {
            base.OnStartClient();

            if (IsOwner == false)
                return;

            _subscription = _health.IsDeath.Where(x => x).Subscribe(_ => OnDied());
        }

        private void OnDestroy() => _subscription?.Dispose();

        #endregion

        private void OnDied()
        {
            _subscription?.Dispose();
            _battle.Leave();
        }
    }
}