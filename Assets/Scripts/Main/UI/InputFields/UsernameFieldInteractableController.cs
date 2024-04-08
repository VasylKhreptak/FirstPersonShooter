using FishNet.Object;
using Zenject;

namespace Main.UI.InputFields
{
    public class UsernameFieldInteractableController : NetworkBehaviour
    {
        private UsernameInputField _usernameInputField;

        [Inject]
        private void Constructor(UsernameInputField usernameInputField)
        {
            _usernameInputField = usernameInputField;
        }

        public override void OnStartClient()
        {
            base.OnStartClient();

            _usernameInputField.Interactable = false;
        }

        public override void OnStopClient()
        {
            base.OnStopClient();

            _usernameInputField.Interactable = true;
        }
    }
}