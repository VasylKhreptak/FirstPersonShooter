using System;
using Data;
using FishNet.Object;
using Infrastructure.Services.Log.Core;
using Networking.Messaging.Chat;
using UI;
using UI.Buttons;
using UI.Dropdowns;
using UI.InputFields;
using UI.InputFields.Username;
using UniRx;
using Zenject;

namespace Networking
{
    public class ClientLifetimeProcessor : NetworkBehaviour
    {
        private Chat _chat;
        private ClientsData _clientsData;
        private UsernameInputField _usernameInputField;
        private Battle _battle;
        private JoinBattleButton _joinBattleButton;
        private LeaveBattleButton _leaveBattleButton;
        private KillsBox.KillsBox _killsBox;
        private ConnectionDropdown _connectionDropdown;
        private AddressInputField _addressInputField;
        private ILogService _logService;
        private PlayInformation _playInformation;

        [Inject]
        private void Constructor(Chat chat, ClientsData clientsData, UsernameInputField usernameInputField, Battle battle,
            JoinBattleButton joinBattleButton, LeaveBattleButton leaveBattleButton, KillsBox.KillsBox killsBox,
            ConnectionDropdown connectionDropdown, AddressInputField addressInputField, ILogService logService,
            PlayInformation playInformation)
        {
            _chat = chat;
            _clientsData = clientsData;
            _usernameInputField = usernameInputField;
            _battle = battle;
            _joinBattleButton = joinBattleButton;
            _leaveBattleButton = leaveBattleButton;
            _killsBox = killsBox;
            _connectionDropdown = connectionDropdown;
            _addressInputField = addressInputField;
            _logService = logService;
            _playInformation = playInformation;
        }

        public override void OnStartClient()
        {
            base.OnStartClient();

            CreateClientDataDelayed();
            _chat.Enabled = true;
            _usernameInputField.Interactable = false;
            _joinBattleButton.Interactable = true;
            _killsBox.Enabled = true;
            _connectionDropdown.Interactable = false;
            _addressInputField.Interactable = false;
            _playInformation.Enabled = true;
            _logService.Log("Client started");
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
            _connectionDropdown.Interactable = true;
            _addressInputField.Interactable = true;
            _playInformation.Enabled = false;
            _logService.Log("Client stopped");
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