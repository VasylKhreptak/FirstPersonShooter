using Networking;
using UI.Buttons.Core;
using Zenject;

namespace UI.Buttons
{
    public class LeaveBattleButton : BaseButton
    {
        private Battle _battle;

        [Inject]
        private void Constructor(Battle battle)
        {
            _battle = battle;
        }

        public bool Interactable
        {
            get => Button.interactable;
            set => Button.interactable = value;
        }

        private void Awake() => Interactable = false;

        protected override void OnClicked() => _battle.Leave();
    }
}