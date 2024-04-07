using FishNet.Object;

namespace Networking
{
    public class GameObjectOwnershipResolver : NetworkBehaviour
    {
        public override void OnStartClient()
        {
            base.OnStartClient();

            gameObject.SetActive(IsOwner);
        }
    }
}