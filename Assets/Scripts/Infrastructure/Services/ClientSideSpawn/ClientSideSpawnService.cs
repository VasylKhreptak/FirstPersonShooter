using FishNet;
using FishNet.Connection;
using FishNet.Object;
using Infrastructure.Data.Static;
using Infrastructure.Data.Static.Core;
using Infrastructure.Services.StaticData.Core;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.ClientSideSpawn
{
    public class ClientSideSpawnService : NetworkBehaviour
    {
        private GamePrefabs _prefabs;

        [Inject]
        private void Constructor(IStaticDataService staticDataService)
        {
            _prefabs = staticDataService.Prefabs;
        }

        public void Spawn(Prefab prefab, Vector3 position, Quaternion rotation)
        {
            SpawnLocally(prefab, position, rotation);
            SpawnOnClientsCmd(prefab, position, rotation);
        }

        private void SpawnLocally(Prefab prefab, Vector3 position, Quaternion rotation)
        {
            GameObject instance = Instantiate(_prefabs[prefab], position, rotation);
            Transform instanceTransform = instance.transform;
            instanceTransform.position = position;
            instanceTransform.rotation = rotation;
        }

        [ServerRpc(RequireOwnership = false)]
        private void SpawnOnClientsCmd(Prefab prefab, Vector3 position, Quaternion rotation, NetworkConnection connection = null) =>
            SpawnOnClients(prefab, position, rotation, connection);

        [ObserversRpc]
        private void SpawnOnClients(Prefab prefab, Vector3 position, Quaternion rotation, NetworkConnection connection)
        {
            if (connection.ClientId == InstanceFinder.ClientManager.Connection.ClientId)
                return;

            SpawnLocally(prefab, position, rotation);
        }
    }
}