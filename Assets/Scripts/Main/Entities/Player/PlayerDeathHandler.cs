using System;
using Data;
using FishNet;
using FishNet.Object;
using Networking;
using Networking.KillsBox;
using UniRx;
using UnityEngine;
using Zenject;

namespace Main.Entities.Player
{
    public class PlayerDeathHandler : NetworkBehaviour
    {
        [Header("References")]
        [SerializeField] private Health.Health _health;
        [SerializeField] private PlayerHitBox _hitBox;

        private Battle _battle;
        private KillsBox _killsBox;
        private ClientsData _clientsData;

        [Inject]
        private void Constructor(Battle battle, KillsBox killsBox, ClientsData clientsData)
        {
            _battle = battle;
            _killsBox = killsBox;
            _clientsData = clientsData;
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

            TryRegisterKill();

            _battle.Leave();
        }

        private void TryRegisterKill()
        {
            if (_hitBox.LastPlayerDamage == null)
                return;

            KillData killData = new KillData
            {
                KillerUsername = _hitBox.LastPlayerDamage.Username, VictimUsername = GetUsername()
            };

            _killsBox.RegisterKill(killData);
        }

        private string GetUsername() => _clientsData.Get(InstanceFinder.ClientManager.Connection.ClientId).Username;
    }
}