using UnityEngine.InputSystem;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public PlayerControls inputActions;
    public InputAction MoveUp;
    public InputAction MoveDown;
    public InputAction MoveLeft;
    public InputAction MoveRight;
    public InputAction Throw;

    private void Awake()
    {
        inputActions = new PlayerControls();
    }

    private void OnEnable()
    {
        MoveUp = inputActions.KeyboardMouse.MoveUp;
        MoveUp.Enable();

        MoveDown = inputActions.KeyboardMouse.MoveDown;
        MoveDown.Enable();

        MoveLeft = inputActions.KeyboardMouse.MoveLeft;
        MoveLeft.Enable();

        MoveRight = inputActions.KeyboardMouse.MoveRight;
        MoveRight.Enable();

        Throw = inputActions.KeyboardMouse.Throw;
        Throw.Enable();
        Throw.started += player.GetComponent<PlayerThrowable>().performAim;
        Throw.canceled += player.GetComponent<PlayerThrowable>().performThrow;
    }

    private void OnDisable()
    {
        MoveUp.Disable();
        MoveDown.Disable();
        MoveLeft.Disable();
        MoveRight.Disable();
        Throw.Disable();
    }
}
