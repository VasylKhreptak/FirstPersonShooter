using Extensions;
using FishNet;
using FishNet.Connection;
using FishNet.Object;
using Infrastructure.Data.Static;
using Infrastructure.Services.StaticData.Core;
using UnityEngine;
using Zenject;
using AudioType = Infrastructure.Data.Static.Core.AudioType;

namespace Infrastructure.Services.Audio
{
    public class AudioService : NetworkBehaviour
    {
        [Header("Preferences")]
        [SerializeField] private GameObject _audioSourcePrefab;

        private IStaticDataService _staticDataService;

        [Inject]
        private void Constructor(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public void Play(AudioType audioType, Vector3 position = default, float volume = 1f, bool onlyLocal = false)
        {
            PlayLocal(audioType, position, volume, _staticDataService.AudioConfig[audioType].Settings.SpatialBlendLocal);

            if (onlyLocal)
                return;

            PlayOnClientsCmd(audioType, position, volume);
        }

        private void PlayLocal(AudioType audioType, Vector3 position, float volume = 1f, float spatialBlend = 1f)
        {
            GameObject audioObject = Instantiate(_audioSourcePrefab, position, Quaternion.identity);
            AudioConfig config = _staticDataService.AudioConfig[audioType];
            AudioSource source = audioObject.GetComponent<AudioSource>();
            source.clip = config.Clips.Random();
            source.volume = volume;
            source.spatialBlend = spatialBlend;
            source.rolloffMode = config.Settings.RolloffMode;
            source.minDistance = config.Settings.MinDistance;
            source.maxDistance = config.Settings.MaxDistance;
            source.Play();
            Destroy(audioObject, source.clip.length);
        }

        [ServerRpc(RequireOwnership = false)]
        private void PlayOnClientsCmd(AudioType audioType, Vector3 position, float volume = 1f, NetworkConnection connection = null) =>
            PlayOnClients(audioType, position, connection, volume);

        [ObserversRpc]
        private void PlayOnClients(AudioType audioType, Vector3 position, NetworkConnection connection, float volume = 1f)
        {
            if (connection.ClientId == InstanceFinder.ClientManager.Connection.ClientId)
                return;

            PlayLocal(audioType, position, volume, _staticDataService.AudioConfig[audioType].Settings.SpatialBlendClients);
        }
    }
}