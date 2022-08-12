using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfTemplate : MonoBehaviour
{
    public List<Vector2> getShapeInfo(Shelf.ShelfShapeType shelfType)
    {
        if (shelfType == Shelf.ShelfShapeType.Single)
        {
            List<Vector2> x = new List<Vector2>();
            x.Add(new Vector2(0, 0));
            return x;
        }
        else if (shelfType == Shelf.ShelfShapeType.Double)
        {
            List<Vector2> x = new List<Vector2>();
            x.Add(new Vector2(0, 0));
            x.Add(new Vector2(1, 0));
            return x;
        }
        else if (shelfType == Shelf.ShelfShapeType.TripleLine)
        {
            List<Vector2> x = new List<Vector2>();
            x.Add(new Vector2(-1, 0));
            x.Add(new Vector2(0, 0));
            x.Add(new Vector2(1, 0));
            return x;
        }
        else if (shelfType == Shelf.ShelfShapeType.TripleLShape)
        {
            List<Vector2> x = new List<Vector2>();
            x.Add(new Vector2(0, 0));
            x.Add(new Vector2(1, 0));
            x.Add(new Vector2(1, 1));
            return x;
        }
        else if (shelfType == Shelf.ShelfShapeType.QuadrupleLine)
        {
            List<Vector2> x = new List<Vector2>();
            x.Add(new Vector2(0, 0));
            x.Add(new Vector2(1, 0));
            x.Add(new Vector2(2, 0));
            x.Add(new Vector2(-1, 0));
            return x;
        }
        else if (shelfType == Shelf.ShelfShapeType.QuadrupleLLeft)
        {
            List<Vector2> x = new List<Vector2>();
            x.Add(new Vector2(0, 0));
            x.Add(new Vector2(1, 0));
            x.Add(new Vector2(-1, 1));
            x.Add(new Vector2(-1, 0));
            return x;
        }
        else if (shelfType == Shelf.ShelfShapeType.QuadrupleLRight)
        {
            List<Vector2> x = new List<Vector2>();
            x.Add(new Vector2(0, 0));
            x.Add(new Vector2(1, 0));
            x.Add(new Vector2(1, 1));
            x.Add(new Vector2(-1, 0));
            return x;
        }
        else if (shelfType == Shelf.ShelfShapeType.QuadrupleSLeft)
        {
            List<Vector2> x = new List<Vector2>();
            x.Add(new Vector2(0, 0));
            x.Add(new Vector2(1, 0));
            x.Add(new Vector2(0, 1));
            x.Add(new Vector2(-1, 1));
            return x;
        }
        else if (shelfType == Shelf.ShelfShapeType.QuadrupleSRight)
        {
            List<Vector2> x = new List<Vector2>();
            x.Add(new Vector2(0, 0));
            x.Add(new Vector2(-1, 0));
            x.Add(new Vector2(0, 1));
            x.Add(new Vector2(1, 1));
            return x;
        }
        else if (shelfType == Shelf.ShelfShapeType.QuadrupleSquare)
        {
            List<Vector2> x = new List<Vector2>();
            x.Add(new Vector2(0, 0));
            x.Add(new Vector2(1, 0));
            x.Add(new Vector2(0, 1));
            x.Add(new Vector2(1, 1));
            return x;
        }
        else if (shelfType == Shelf.ShelfShapeType.QuadrupleTShape)
        {
            List<Vector2> x = new List<Vector2>();
            x.Add(new Vector2(0, 0));
            x.Add(new Vector2(1, 0));
            x.Add(new Vector2(-1, 0));
            x.Add(new Vector2(0, -1));
            return x;
        }
        return null;
    }
}
