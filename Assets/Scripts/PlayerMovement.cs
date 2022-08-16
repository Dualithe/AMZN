using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IKnockbackable
{
    [SerializeField] private float playerSpeed;
    
    private Rigidbody2D rb;
    public Rigidbody2D body => rb;

    private Animator playerAnimator;
    private InputHandler ih;
    private Vector2 moveDirection = Vector2.zero;

    private void Awake() {
        playerAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        ih = GameObject.FindWithTag("InputHandler").GetComponent<InputHandler>();

        // register input

    }

    private void FixedUpdate()
    {
        moveDirection = new Vector2(
            ih.event_MoveRight.ReadValue<float>() - ih.event_MoveLeft.ReadValue<float>(), 
            ih.event_MoveUp.ReadValue<float>() - ih.event_MoveDown.ReadValue<float>()
        );

        rb.velocity = moveDirection * playerSpeed;

        if (rb.velocity.x < 0.0 && transform.position.x < -14.0) {
            rb.velocity *= Vector2.up;
        }
        else if (rb.velocity.x > 0.0 && transform.position.x > 14.0) {
            rb.velocity *= Vector2.up;
        }

        if (rb.velocity.y < 0.0 && transform.position.y < -8.0) {
            rb.velocity *= Vector2.right;
        }
        else if (rb.velocity.y > 0.0 && transform.position.y > 8.0) {
            rb.velocity *= Vector2.right;
        }

        var isRunning = Mathf.Abs(moveDirection.x) > 0 || Mathf.Abs(moveDirection.y) > 0;
        playerAnimator.SetBool("IsRunning", isRunning);
    }

    public void Knockback(Vector2 dir, float force)
    {
        rb.AddForce(dir * force, ForceMode2D.Impulse);
    }
}
