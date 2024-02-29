using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJoinController : MonoBehaviour
{
    PlayerJoin[] playerJoins;

    List<InputDevice> pairedDevices = new List<InputDevice>();

    private MenuToGameData MenuToGameData => FindObjectOfType<MenuToGameData>();

    private int playerNecessary = 1;

    int selector = 0;

    private void Start()
    {
        if (!MenuToGameDataExists()) return;

        playerJoins = GetComponentsInChildren<PlayerJoin>(true);
        for (int i = 0; i < playerJoins.Length; i++)
        {
            playerJoins[i].PlayerIndex = i;
        }

        SetNecessaryPlayerAmout();
    }

    private void Update()
    {
        CycleSelector();
        CheckForPlayerJoin();
        CheckForPlayerLeave();

        if (CheckGameReady()) 
        {
            if (ReadGameStartButton()) 
            {
                LoadGameSceneWithSettings();
            }
        }

    }

    private void SetNecessaryPlayerAmout()
    {
        switch (MenuToGameData.GameMode)
        {
            case GameModeType.Score:
                playerNecessary = 2;
                break;
            case GameModeType.Time:
                playerNecessary = 1;
                break;
        }
    }

    private bool MenuToGameDataExists()
    {
        if (MenuToGameData != null) return true;
        GetComponent<SceneSwitcher>().LoadScene(0);
        return false;
    }

    private void LoadGameSceneWithSettings()
    {
        foreach (var playerJoin in playerJoins)
        {
            if (playerJoin.InputDevice == null) continue;

            PlayerDTO playerDTO = new PlayerDTO();

            playerDTO.InputDevice = playerJoin.InputDevice;
            playerDTO.ColorType = playerJoin.ColorType;
            playerDTO.PlayerIndex = playerJoin.PlayerIndex;

            MenuToGameData.PlayerDTOs.Add(playerDTO);
        }

        if (MenuToGameData.GameMode == GameModeType.Golf && MenuToGameData.PlayerDTOs.Count > 1)
        {
            MenuToGameData.GameMode = GameModeType.MultiGolf;
        }

        GetComponent<SceneSwitcher>().LoadScene(MenuToGameData.sceneIndexToLoad);
    }

    private bool ReadGameStartButton()
    {
        foreach (InputDevice device in pairedDevices)
        {
            if (InputDeviceManager.Instance.GetMenuInputAction(device) == MenuInputActionTypes.Start)
            { return true; }
        }
        return false;
    }

    private bool CheckGameReady()
    {
        int readyCount = 0;

        foreach (var playerJoin in playerJoins) 
        {
            if (playerJoin.InputDevice != null) 
            {
                readyCount++;
            }
        }

        if (readyCount >= playerNecessary) return true;
        return false;
    }

    private void CycleSelector()
    {
        if (selector < 0) 
        {
            selector = 0;
        }
        
        if (selector > playerJoins.Length - 1) 
        {
            selector = playerJoins.Length - 1;
        }

        if (playerJoins[selector].InputDevice != null)
        {
            for (int i = 0; i < playerJoins.Length - 1; i++) 
            {
                selector = i;
                if (playerJoins[selector].InputDevice == null)
                {
                    return;
                }
            }
        }
    }

    private void CheckForPlayerJoin()
    {
        foreach (InputDevice device in InputDeviceManager.Instance.ManagedInputDevices)
        {
            if (InputDeviceManager.Instance.GetMenuInputAction(device) == MenuInputActionTypes.Confirm)
            {
                if (playerJoins[selector].InputDevice == null && !pairedDevices.Contains(device))
                {
                    playerJoins[selector].InputDevice = device;
                    pairedDevices.Add(device);

                    selector++;
                    return;
                }
            }
        }
    }

    private void CheckForPlayerLeave()
    {
        foreach (var playerJoin in playerJoins)
        {
            InputDevice device = playerJoin.InputDevice;

            if (InputDeviceManager.Instance.GetMenuInputAction(device) == MenuInputActionTypes.Cancel)
            {
                playerJoin.InputDevice = null;
                pairedDevices.Remove(device);
                selector--;
                break;
            }
        }
    }
}
