using FishNet.Object;
using UnityEngine;

namespace Networking
{
    public class GameObjectOwnershipResolver : NetworkBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject _gameObject;

        [Header("Preferences")]
        [SerializeField] private bool _inversed;

        public override void OnStartClient()
        {
            base.OnStartClient();

            _gameObject.SetActive(_inversed ? IsOwner : !IsOwner);
        }
    }
}