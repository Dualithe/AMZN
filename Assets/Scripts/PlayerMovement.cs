using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IKnockbackable
{
    private GameObject player = null;
    public Rigidbody2D body => rb;
    private Rigidbody2D rb;
    private Animator playerAnimator;
    [SerializeField] private Animator armAnimator;
    [SerializeField] private float playerSpeed;
    [SerializeField] private InputHandler ih;
    Vector2 moveDirection = Vector2.zero;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        ih = GameObject.FindWithTag("InputHandler").GetComponent<InputHandler>();
        player = gameObject;
    }

    private void FixedUpdate()
    {
        moveDirection = new Vector2(ih.MoveRight.ReadValue<float>() - ih.MoveLeft.ReadValue<float>(), ih.MoveUp.ReadValue<float>() - ih.MoveDown.ReadValue<float>());

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
            playerAnimator.SetBool("IsRunning", true);
            armAnimator.SetBool("IsRunning", true);
        }
        else
        {
            playerAnimator.SetBool("IsRunning", false);
            armAnimator.SetBool("IsRunning", false);
        }
    }

    public void isHolding(bool ih)
    {
        armAnimator.SetBool("IsHolding", ih);
    }

    public void Knockback(Vector2 dir, float force)
    {
        rb.AddForce(dir * force, ForceMode2D.Impulse);
    }
}
