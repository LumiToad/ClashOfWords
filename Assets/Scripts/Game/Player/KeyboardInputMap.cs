using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardInputMap : MonoBehaviour
{
    private Keyboard keyboard;
    public Keyboard Keyboard
    {
        get { return keyboard; } 
        set { keyboard = value; }
    }

    private PlayerActions movementActions;

    private void Awake()
    {
        movementActions = GetComponent<PlayerActions>();
    }

    private void Update()
    {
        if (keyboard == null) return;

        movementActions.MoveAction(MoveInput());

        if (AttackInput())
        {
            movementActions.TailAction();
        }

        if(NutcrackerInput()) 
        {
            movementActions.NutcrackerAction();
        }
    }

    private Vector3 MoveInput()
    {
        Vector3 vector3 = Vector3.zero;

        if (keyboard[Key.W].isPressed)
        {
            vector3.z = 1.0f;
        }

        if (keyboard[Key.A].isPressed)
        {
            vector3.x = -1.0f;
        }

        if (keyboard[Key.S].isPressed)
        {
            vector3.z = -1.0f;
        }

        if (keyboard[Key.D].isPressed)
        {
            vector3.x = 1.0f;
        }

        return vector3;
    }

    private bool AttackInput()
    {
        if (keyboard[Key.Space].wasPressedThisFrame)
        {
            return true;
        }
        
        return false;
    }

    private bool NutcrackerInput()
    {
        if (keyboard[Key.E].wasPressedThisFrame)
        {
            return true;
        }

        return false;
    }
}
