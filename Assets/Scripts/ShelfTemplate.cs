using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShelfTemplates
{
    private static Dictionary<Shelf.ShelfShapeType, List<Vector2>> templates = new Dictionary<Shelf.ShelfShapeType, List<Vector2>>();

    static ShelfTemplates() {
        _DefineShape(Shelf.ShelfShapeType.Single, new(){
            (0, 0)
        });
        _DefineShape(Shelf.ShelfShapeType.Double, new(){
            (0, 0),(1, 0)
        });
        _DefineShape(Shelf.ShelfShapeType.TripleLine, new(){
            (-1, 0),(0, 0),(1, 0)
        });
        _DefineShape(Shelf.ShelfShapeType.TripleLShape, new(){
            (0, 0),(1, 0),(1, 1)
        });
        _DefineShape(Shelf.ShelfShapeType.QuadrupleLine, new(){
            (0, 0),(1, 0),(2, 0),(-1, 0)
        });
        _DefineShape(Shelf.ShelfShapeType.QuadrupleLLeft, new(){
            (0, 0),(1, 0),(-1, 1),(-1, 0)
        });
        _DefineShape(Shelf.ShelfShapeType.QuadrupleLRight, new(){
            (0, 0),(1, 0),(1, 1),(-1, 0)
        });
        _DefineShape(Shelf.ShelfShapeType.QuadrupleSLeft, new(){
            (0, 0),(1, 0),(0, 1),(-1, 1)
        });
        _DefineShape(Shelf.ShelfShapeType.QuadrupleSRight, new(){
            (0, 0),(-1, 0),(0, 1),(1, 1)
        });
        _DefineShape(Shelf.ShelfShapeType.QuadrupleSquare, new(){
            (0, 0),(1, 0),(0, 1),(1, 1)
        });
        _DefineShape(Shelf.ShelfShapeType.QuadrupleTShape, new(){
            (0, 0),(1, 0),(-1, 0),(0, -1)
        });
    }

    public static void _DefineShape(Shelf.ShelfShapeType type, List<(float x, float y)> shape) {
        templates.Add(type, shape.Select(p => new Vector2(p.x, p.y)).ToList());
    } 

    public static List<Vector2> getShapeInfo(Shelf.ShelfShapeType shelfType) {
        return templates.GetValueOrDefault(shelfType);
    }
}
