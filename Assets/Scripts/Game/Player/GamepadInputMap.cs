using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadInputMap : MonoBehaviour
{
    private Gamepad gamepad;
    public Gamepad Gamepad
    {  
        get { return gamepad; }
        set { gamepad = value; }
    }

    private PlayerActions movementActions;

    private float debugDeadzone = 0.45f;

    private void Awake()
    {
        movementActions = GetComponent<PlayerActions>();
    }

    private void Update()
    {
        if (gamepad == null) return;

        movementActions.MoveAction(LStickInput());

        if (AttackInput())
        {
            movementActions.TailAction();
        }

        if (NutcrackerInput()) 
        {
            movementActions.NutcrackerAction();
        }
    }

    private Vector3 LStickInput()
    {
        Vector3 vector3 = Vector3.zero;

        if (gamepad.leftStick.magnitude > debugDeadzone)
        {
            vector3.x = gamepad.leftStick.x.ReadValue();
            vector3.z = gamepad.leftStick.y.ReadValue();
        }

        return vector3;
    }

    private bool AttackInput()
    {
        if (gamepad.aButton.wasPressedThisFrame)
        {
            return true;
        }

        return false;
    }

    private bool NutcrackerInput()
    {
        if (gamepad.xButton.wasPressedThisFrame)
        {
            return true;
        }

        return false;
    }
}
