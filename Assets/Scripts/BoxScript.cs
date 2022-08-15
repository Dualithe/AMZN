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
    private Vector3 scale;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        scale = transform.localScale;
    }

    private void Update()
    {
        if (tweening)
        {
            tweenCD -= Time.deltaTime;
            tweenCD = Mathf.Max(tweenCD, 0);
            tweening = tweenCD >= 0f ? true : false;
        }
    }

    private void OnDestroy()
    {
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
        if (!tweening)
        {
            transform.DOScale(scale * 10f, 0.3f);
            transform.DOScale(scale, 0.3f);
            tweenCD = 0.6f;
        }
    }
}
