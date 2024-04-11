using System;
using Data;
using FishNet.Object;
using Networking.Messaging.Chat;
using UI.Buttons;
using UI.InputFields.Username;
using UniRx;
using Zenject;

namespace Networking
{
    public class ClientLifetimeHandler : NetworkBehaviour
    {
        private Chat _chat;
        private ClientsData _clientsData;
        private UsernameInputField _usernameInputField;
        private Battle _battle;
        private JoinBattleButton _joinBattleButton;
        private LeaveBattleButton _leaveBattleButton;
        private KillsBox.KillsBox _killsBox;

        [Inject]
        private void Constructor(Chat chat, ClientsData clientsData, UsernameInputField usernameInputField, Battle battle,
            JoinBattleButton joinBattleButton, LeaveBattleButton leaveBattleButton, KillsBox.KillsBox killsBox)
        {
            _chat = chat;
            _clientsData = clientsData;
            _usernameInputField = usernameInputField;
            _battle = battle;
            _joinBattleButton = joinBattleButton;
            _leaveBattleButton = leaveBattleButton;
            _killsBox = killsBox;
        }

        public override void OnStartClient()
        {
            base.OnStartClient();

            CreateClientDataDelayed();
            _chat.Enabled = true;
            _usernameInputField.Interactable = false;
            _joinBattleButton.Interactable = true;
            _killsBox.Enabled = true;
        }

        public override void OnStopClient()
        {
            base.OnStopClient();

            _battle.Leave();
            _chat.Enabled = false;
            _usernameInputField.Interactable = true;
            _joinBattleButton.Interactable = false;
            _leaveBattleButton.Interactable = false;
            _killsBox.Enabled = false;
        }

        private void CreateClientDataDelayed()
        {
            Observable
                .Timer(TimeSpan.FromSeconds(0.1f))
                .Subscribe(_ => CreateClientData());
        }

        private void CreateClientData()
        {
            int id = LocalConnection.ClientId;

            ClientData clientData = new ClientData
            {
                Username = _usernameInputField.Text
            };

            _clientsData.Add(id, clientData);
        }
    }
}