using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour, IKnockbackable
{
    private Rigidbody2D rb;
    public Rigidbody2D body => rb;
    private bool attached = false;
    private GameObject attachedTo = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerThrowable>().currBox == null)
        {
            collision.gameObject.GetComponent<PlayerThrowable>().currBox = this;
            transform.parent = collision.transform;
            GetComponent<Collider2D>().isTrigger = true;
            attached = true;
            attachedTo = collision.gameObject;
        }
    }

    private void Update()
    {
        if (attached)
        {
            transform.position = attachedTo.transform.position + new Vector3(0, 1);
        }
    }

    public void Knockback(Vector2 dir, float force)
    {
        rb.AddForce(dir * force, ForceMode2D.Impulse);
    }
}
