using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IKnockbackable
{
    private GameObject player = null;
    public Rigidbody2D body => rb;
    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField] private float playerSpeed;
    Vector2 moveDirection = Vector2.zero;
    public PlayerControls inputActions;
    public InputAction MoveUp;
    public InputAction MoveDown;
    public InputAction MoveLeft;
    public InputAction MoveRight;


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
    }

    private void OnDisable()
    {
        MoveUp.Disable();
        MoveDown.Disable();
        MoveLeft.Disable();
        MoveRight.Disable();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = gameObject;
    }

    private void FixedUpdate()
    {
        moveDirection = new Vector2(MoveRight.ReadValue<float>() - MoveLeft.ReadValue<float>(), MoveUp.ReadValue<float>() - MoveDown.ReadValue<float>());

        if (moveDirection.x < 0)
        {
            player.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            player.GetComponent<SpriteRenderer>().flipX = false;
        }

        rb.velocity = (moveDirection * playerSpeed);




        if (Mathf.Abs(moveDirection.x) > 0 || Mathf.Abs(moveDirection.y) > 0)
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
    }

    public void isHolding(bool ih)
    {
        animator.SetBool("IsHolding", ih);
    }
}
