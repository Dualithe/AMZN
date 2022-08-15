using UnityEngine.InputSystem;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public PlayerControls inputActions;
    public InputAction event_MoveUp;
    public InputAction event_MoveDown;
    public InputAction event_MoveLeft;
    public InputAction event_MoveRight;
    public InputAction event_Throw;
    public InputAction event_Pickup;

    private static InputHandler instance;
    public static InputHandler Instance => instance;
 
    private void Awake()
    {
        inputActions = new PlayerControls();

        instance = this;

        event_MoveUp = inputActions.KeyboardMouse.MoveUp;
        event_MoveDown = inputActions.KeyboardMouse.MoveDown;
        event_MoveLeft = inputActions.KeyboardMouse.MoveLeft;
        event_MoveRight = inputActions.KeyboardMouse.MoveRight;
        event_Pickup = inputActions.KeyboardMouse.Pickup;
        event_Throw = inputActions.KeyboardMouse.Throw;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}
