using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfModule : MonoBehaviour
{
    public bool isFilled = false;
    public void fill(GameObject box)
    {

        isFilled = true;
        Destroy(box);
        var x = Color.cyan;
        x.a = 0.8f;
        GetComponent<SpriteRenderer>().color = x;
    }
}
