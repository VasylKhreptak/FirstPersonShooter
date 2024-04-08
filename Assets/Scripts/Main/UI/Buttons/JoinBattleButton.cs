using Main.UI.Buttons.Core;
using Networking;
using Zenject;

namespace Main.UI.Buttons
{
    public class JoinBattleButton : BaseButton
    {
        private Battle _battle;

        [Inject]
        private void Constructor(Battle battle)
        {
            _battle = battle;
        }

        protected override void OnClicked() => _battle.Join();
    }
}