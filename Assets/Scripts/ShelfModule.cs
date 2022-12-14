using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfModule : MonoBehaviour
{
    [SerializeField] private Sprite spriteFilled;
    public bool isFilled = false;

    public bool IsFilled => isFilled;

    public void fill(GameObject box)
    {
        isFilled = true;
        Destroy(box);
        GetComponent<SpriteRenderer>().sprite = spriteFilled;

        LevelManager.Instance.CheckWinCondition();
    }
}
