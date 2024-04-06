using FishNet.Object;
using Map;
using Zenject;

namespace Networking
{
    public class ClientStarter : NetworkBehaviour
    {
        private MapCamera _mapCamera;
        private PlayerSpawnPoints _playerSpawnPoints;

        [Inject]
        private void Constructor(MapCamera mapCamera, PlayerSpawnPoints playerSpawnPoints)
        {
            _mapCamera = mapCamera;
            _playerSpawnPoints = playerSpawnPoints;
        }

        public override void OnStartClient()
        {
            base.OnStartClient();

            StartClient();
        }

        private void StartClient()
        {
            _mapCamera.Enabled = false;
        }

        private void SpawnPlayer() { }
    }
}