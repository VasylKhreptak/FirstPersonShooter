using Main.UI.Buttons.Core;
using Networking;
using Zenject;

namespace Main.UI.Buttons
{
    public class LeaveBattleButton : BaseButton
    {
        private BattleController _battleController;

        [Inject]
        private void Constructor(BattleController battleController)
        {
            _battleController = battleController;
        }

        protected override void OnClicked() => _battleController.Leave();
    }
}