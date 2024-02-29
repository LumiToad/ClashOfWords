using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum MenuInputActionTypes
{
    None = -1,
    Up = 10,
    Down = 20,
    Left = 30,
    Right = 40,
    Confirm = 50,
    Cancel = 60,
    Start = 70,
    Escape = 80,
}

public class InputDeviceManager : MonoBehaviour
{
    private List<InputDevice> inputDevices;
    public List<InputDevice> ManagedInputDevices { get => inputDevices; }

    private bool isAnyGamepadConnected = false;
    public bool IsAnyGamepadConnected
    { get => isAnyGamepadConnected; private set => isAnyGamepadConnected = value; }

    public static InputDeviceManager Instance;

    private void Awake()
    {
        SetAsSingleton();
        DontDestroyOnLoad(gameObject);
        inputDevices = new List<InputDevice>();
    }

    private void Start()
    {
        Setup();
    }

    private void SetAsSingleton()
    {
        if (Instance != null) 
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Setup()
    {
        inputDevices.Add(Keyboard.current);
        isAnyGamepadConnected = GamepadsToManagedInputDevices();

        InputSystem.onDeviceChange += OnDeviceChanged;
    }

    private bool GamepadsToManagedInputDevices()
    {
        bool retVal = false;

        foreach (Gamepad pad in Gamepad.all)
        {
            inputDevices.Add(pad);
            retVal = true;
        }
        return retVal;
    }

    private void OnDeviceChanged(InputDevice device, InputDeviceChange change)
    {
        if (device is Gamepad)
        {
            Gamepad thisGamepad = device as Gamepad;

            switch (change)
            {
                case InputDeviceChange.Added:
                    ConnectGamepad(thisGamepad);
                    break;
                case InputDeviceChange.Disconnected:
                    DisconnectGamepad(thisGamepad);
                    break;
                case InputDeviceChange.Reconnected:
                    ConnectGamepad(thisGamepad);
                    break;
                case InputDeviceChange.Removed:
                    DisconnectGamepad(thisGamepad);
                    break;
                default:
                    break;
            }
        }
    }

    private void DisconnectGamepad(Gamepad pad)
    {
        if (!inputDevices.Contains(pad)) return;

        inputDevices.Remove(pad);

        if (inputDevices.Count == 0)
        {
            isAnyGamepadConnected = false;
        }
    }

    private void ConnectGamepad(Gamepad pad)
    {
        if (inputDevices.Contains(pad)) return;
        
        inputDevices.Add(pad);
        isAnyGamepadConnected = true;
    }

    public MenuInputActionTypes GetMenuInputAction(InputDevice device)
    {
        MenuInputActionTypes type = MenuInputActionTypes.None;

        if (device is Gamepad)
        {
            GetInputByGamepad(device as Gamepad, ref type);
        }
        else if (device is Keyboard)
        {
            GetInputByKeyboard(device as Keyboard, ref type);
        }

        return type;
    }

    private void GetInputByKeyboard(Keyboard keyboard, ref MenuInputActionTypes type) 
    {
        if (keyboard.wKey.wasPressedThisFrame) 
        {
            type = MenuInputActionTypes.Up;
        }
        if (keyboard.sKey.wasPressedThisFrame)
        {
            type = MenuInputActionTypes.Down;
        }
        if (keyboard.aKey.wasPressedThisFrame)
        {
            type = MenuInputActionTypes.Left;
        }
        if (keyboard.dKey.wasPressedThisFrame)
        {
            type = MenuInputActionTypes.Right;
        }
        if (keyboard.spaceKey.wasPressedThisFrame) 
        {
            type = MenuInputActionTypes.Confirm;
        }
        if (keyboard.backspaceKey.wasPressedThisFrame)
        {
            type = MenuInputActionTypes.Cancel;
        }
        if (keyboard.enterKey.wasPressedThisFrame)
        {
            type = MenuInputActionTypes.Start;
        }
        if (keyboard.escapeKey.wasPressedThisFrame)
        {
            type = MenuInputActionTypes.Escape;
        }
    }

    private void GetInputByGamepad(Gamepad pad, ref MenuInputActionTypes type)
    {
        if (pad.leftStick.up.wasPressedThisFrame || pad.dpad.up.wasPressedThisFrame)
        {
            type = MenuInputActionTypes.Up;
        }
        if (pad.leftStick.down.wasPressedThisFrame || pad.dpad.down.wasPressedThisFrame)
        {
            type = MenuInputActionTypes.Down;
        }
        if (pad.leftStick.left.wasPressedThisFrame || pad.dpad.left.wasPressedThisFrame)
        {
            type = MenuInputActionTypes.Left;
        }
        if (pad.leftStick.right.wasPressedThisFrame || pad.dpad.right.wasPressedThisFrame)
        {
            type = MenuInputActionTypes.Right;
        }
        if (pad.aButton.wasPressedThisFrame)
        {
            type = MenuInputActionTypes.Confirm;
        }
        if (pad.bButton.wasPressedThisFrame)
        {
            type = MenuInputActionTypes.Cancel;
        }
        if (pad.startButton.wasPressedThisFrame)
        {
            type = MenuInputActionTypes.Start;
        }
        if (pad.selectButton.wasPressedThisFrame)
        {
            type = MenuInputActionTypes.Escape;
        }
    }
}
