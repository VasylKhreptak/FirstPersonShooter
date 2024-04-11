using FishNet.Object;
using UnityEngine;

namespace Networking
{
    public class NetworkAudio : NetworkBehaviour
    {
        [Header("References")]
        [SerializeField] private AudioSource _source;

        #region MonoBehaviour

        protected override void OnValidate()
        {
            base.OnValidate();

            _source ??= GetComponent<AudioSource>();
        }

        #endregion

        #region Networking

        public override void OnStartClient()
        {
            base.OnStartClient();

            enabled = IsOwner;
        }

        #endregion

        public void Play()
        {
            PlayLocally();
            PlayOnClientsServer();
        }

        private void PlayLocally() => _source.Play();

        [ServerRpc]
        private void PlayOnClientsServer() => PlayOnClients();

        [ObserversRpc(ExcludeOwner = true)]
        private void PlayOnClients() => PlayLocally();
    }
}