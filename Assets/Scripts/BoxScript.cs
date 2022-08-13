using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour, IKnockbackable
{
    private Rigidbody2D rb;
    public Rigidbody2D body => rb;
    private bool attached = false;
    private GameObject attachedTo = null;
    private GameObject player;
    public float attachCD;
    public float collisionCD;
    public bool called = true;
    private bool isDestroyed = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
    }
    public void pickMeUp()
    {
        player.gameObject.GetComponent<PlayerThrowable>().currBox = this;
        transform.parent = player.transform;
        attached = true;
        attachedTo = player.gameObject;
    }

    private void Update()
    {
        attachCD = Mathf.Max(attachCD - Time.deltaTime, 0);
        collisionCD = Mathf.Max(collisionCD - Time.deltaTime, 0);

        if (collisionCD == 0 && called == false)
        {
            gameObject.layer -= 1;
            called = true;
        }

        if (attached)
        {
            transform.position = attachedTo.transform.position + new Vector3(0, 0.7f);
        }
    }

    public void Knockback(Vector2 dir, float force)
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(dir * force, ForceMode2D.Impulse);
    }

    public void clearAttached()
    {
        attached = false;
        attachedTo.gameObject.GetComponent<PlayerThrowable>().currBox = null;
        attachedTo = null;
        transform.parent = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var shelfModule = collision.gameObject.GetComponent<ShelfModule>();
        if (collision.transform.tag == "ShelfModule" && !shelfModule.isFilled && !isDestroyed)
        {
            isDestroyed = true;
            shelfModule.fill(gameObject);
            if (attachedTo != null)
            {
                attachedTo.gameObject.GetComponent<PlayerThrowable>().pm.isHolding(false);
            }
        }
    }
}
