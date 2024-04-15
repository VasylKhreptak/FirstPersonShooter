using Networking;
using UI.Buttons.Core;
using UnityEngine;
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

        #region MonoBehaviour

        private void Awake() => Interactable = false;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
                _battle.Leave();
        }

        #endregion

        protected override void OnClicked() => _battle.Leave();
    }
}