using System;
using FishNet.Object;
using Main.Health;
using UnityEngine;

namespace Main.Weapons.Core
{
    public class ShootProcessor : NetworkBehaviour
    {
        public void Shoot(Ray ray, float damage) => ShootServer(ray, damage);

        [ServerRpc(RequireOwnership = false)]
        private void ShootServer(Ray ray, float damage)
        {
            if (Physics.Raycast(ray, out RaycastHit hitInfo) == false)
                return;

            if (hitInfo.collider.TryGetComponent(out IDamageable damageable))
                damageable.TakeDamage(damage);

            Debug.Log(hitInfo.collider.gameObject.name);
        }
    }
}