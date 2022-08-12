using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfModule : MonoBehaviour
{
    bool isFilled = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Box")
        {
            isFilled = true;
            Destroy(collision.gameObject);
            var x = Color.cyan;
            x.a = 0.8f;
            GetComponent<SpriteRenderer>().color = x;
        }
    }
}
