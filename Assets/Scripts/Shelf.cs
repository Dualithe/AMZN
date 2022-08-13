using UnityEngine;

public class Shelf : MonoBehaviour
{
    [SerializeField] private ShelfTemplate shelfTemplate;
    [SerializeField] private ShelfModule module;
    public enum ShelfShapeType { Single, Double, TripleLine, TripleLShape, QuadrupleLine, QuadrupleSquare, QuadrupleLLeft, QuadrupleLRight, QuadrupleSLeft, QuadrupleSRight, QuadrupleTShape }
    [SerializeField] private ShelfShapeType shelfType;


    private void Start()
    {
        var shapeCoordinates = shelfTemplate.getShapeInfo(shelfType);

        for (int i = 0; i < shapeCoordinates.Count; i++)
        {
            Instantiate(module, shapeCoordinates[i], Quaternion.identity);
        }
    }

    private void OnDrawGizmos()
    {
        var shapeCoordinates = shelfTemplate.getShapeInfo(shelfType);

        for (int i = 0; i < shapeCoordinates.Count; i++)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(shapeCoordinates[i], Vector3.one);
        }
    }
}