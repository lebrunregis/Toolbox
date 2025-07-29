using DataStore.Data;
using DataStore.Runtime;
using DebugBehaviour.Runtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LocalMultiplayerPlayerManager.Runtime
{
    [RequireComponent(typeof(PlayerInputManager))]
    public class LocalPlayerLobby : VerboseMonoBehaviour
    {
        public List<PlayerInput> players;
        private PlayerInputManager inputManager;

        private void Start()
        {
            inputManager = GetComponent<PlayerInputManager>();
            inputManager.onPlayerJoined += OnPlayerJoined;
            inputManager.onPlayerLeft += OnPlayerLeft;
            DataStoreManager.RuntimeDataStore.Set<List<PlayerInput>>(DataCategoryEnum.Runtime, "PlayerInputList", players);
        }

        public void OnPlayerJoined(PlayerInput playerInput)
        {

            if (playerInput.TryGetComponent<LocalMultiplayerPlayer>(out LocalMultiplayerPlayer player))
            {
                player.name = inputManager.name;
                gameObject.name = playerInput.user.platformUserAccountName;
            }
            Log($"Player {playerInput.playerIndex + 1} joined.");
            Log($"{playerInput.user}");
        }

        public void OnPlayerLeft(PlayerInput playerInput)
        {
            players.Remove(playerInput);
            Log($"Player {playerInput.playerIndex + 1} left.");
            Log($"{playerInput.user}");
        }
    }
}
