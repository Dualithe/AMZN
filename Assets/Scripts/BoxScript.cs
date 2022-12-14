using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoxScript : MonoBehaviour, IKnockbackable
{
    private Rigidbody2D rb;
    public Rigidbody2D body => rb;
    private bool isDestroyed = false;
    private bool tweening = false;
    private float tweenCD;
    public bool wasThrown = false;
    private Vector3 scale;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        scale = transform.localScale;
    }

    private void Update()
    {
        if (rb.velocity.magnitude == 0)
        {
            wasThrown = false;
        }

        if (tweening)
        {
            tweenCD -= Time.deltaTime;
            tweenCD = Mathf.Max(tweenCD, 0);
            tweening = tweenCD >= 0f ? true : false;
        }

        if (transform.position.x < -13.5) {
            var pos = rb.transform.position;
            pos.x = -13.5f;
            rb.transform.position = pos;
        }
        else if (transform.position.x > 13.5) {
            var pos = rb.transform.position;
            pos.x = 13.5f;
            rb.transform.position = pos;
        }

        if (transform.position.y < -8.0) {
            var pos = rb.transform.position;
            pos.y = -8.0f;
            rb.transform.position = pos;
        }
        else if (transform.position.y > 8.0) {
            var pos = rb.transform.position;
            pos.y = 8.0f;
            rb.transform.position = pos;
        }

    }

    private void OnDestroy()
    {
        Level.Current?.UpdateBoxList();
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
        if (collision.transform.tag == "Player" && wasThrown == true && collision.gameObject.GetComponent<PlayerThrowable>().pickedUpBox != null)
        {
            collision.gameObject.GetComponent<PlayerThrowable>().ThrowBox((collision.transform.position - transform.position).normalized * 8f);
        }
        if (!tweening)
        {
            transform.DOScale(scale * 10f, 0.3f);
            transform.DOScale(scale, 0.3f);
            tweenCD = 0.6f;
        }
    }
}
