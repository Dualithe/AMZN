using UnityEngine;

public class Shelf : MonoBehaviour
{
    [SerializeField] private ShelfTemplate shelfTemplate;
    [SerializeField] private ShelfModule module;
    public enum ShelfShapeType { Single, Double, TripleLine, TripleLShape, QuadrupleLine, QuadrupleSquare, QuadrupleLLeft, QuadrupleLRight, QuadrupleSLeft, QuadrupleSRight, QuadrupleTShape }
    [SerializeField] private ShelfShapeType shelfType;


    private void Start()
    {
        Vector2 pos = transform.position;
        var shapeCoordinates = shelfTemplate.getShapeInfo(shelfType);

        for (int i = 0; i < shapeCoordinates.Count; i++)
        {
            Instantiate(module, pos + shapeCoordinates[i], Quaternion.identity);
        }
    }

    private void OnDrawGizmos()
    {
        Vector2 pos = transform.position;
        var shapeCoordinates = shelfTemplate.getShapeInfo(shelfType);

        for (int i = 0; i < shapeCoordinates.Count; i++)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(pos + shapeCoordinates[i], Vector3.one);
        }
    }
}