using System;
using FishNet.Object;
using UI;
using UniRx;
using UnityEngine;
using Zenject;

namespace Main.Entities.Player
{
    public class PlayerHealthViewUpdater : NetworkBehaviour
    {
        [Header("References")]
        [SerializeField] private Health.Health _health;

        private HealthView _healthView;

        [Inject]
        private void Constructor(HealthView healthView)
        {
            _healthView = healthView;
        }

        private IDisposable _subscription;

        public override void OnStartClient()
        {
            base.OnStartClient();

            if (IsOwner == false)
            {
                enabled = false;
                return;
            }

            _subscription = _health.Fill.Subscribe(OnHealthFillChanged);
        }

        private void OnDestroy() => _subscription?.Dispose();

        private void OnHealthFillChanged(float fill) => _healthView.SetHealthFill(fill);
    }
}