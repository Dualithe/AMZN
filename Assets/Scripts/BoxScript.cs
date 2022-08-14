using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour, IKnockbackable
{
    private Rigidbody2D rb;
    public Rigidbody2D body => rb;
    private bool isDestroyed = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void OnDestroy() {
        Level.Current.UpdateBoxList();
    }

    public void Knockback(Vector2 dir, float force)
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(dir * force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var shelfModule = collision.gameObject.GetComponent<ShelfModule>();
        if (collision.transform.tag == "ShelfModule" && !shelfModule.isFilled && !isDestroyed)
        {
            isDestroyed = true;
            shelfModule.fill(gameObject);
        }
    }
}
