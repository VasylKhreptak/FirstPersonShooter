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

        private BattleController _battleController;

        [Inject]
        private void Constructor(BattleController battleController)
        {
            _battleController = battleController;
        }

        private IDisposable _subscription;

        #region MonoBehaviour

        private void OnEnable()
        {
            enabled = IsOwner;

            _subscription = _health.Value.Where(x => x == 0).Subscribe(_ => OnDied());
        }

        private void OnDisable() => _subscription?.Dispose();

        #endregion

        private void OnDied()
        {
            _subscription.Dispose();
            _battleController.Leave();
        }
    }
}