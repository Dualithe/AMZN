using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using TMPro;

public class Level : MonoBehaviour
{
    public static Level Current => LevelManager.Instance.CurrentLevel;

    public List<float> timeRequiredForStars = new List<float>(4);

    [Space]
    [SerializeField] private PlayerMovement player;
    [SerializeField] private BoxScript boxPrefab;
    [SerializeField] private Transform boxesParent;
    [SerializeField] private NavMeshSurface navMesh;

    private List<BoxScript> boxList = new();

    public PlayerMovement Player => player;
    public IReadOnlyList<BoxScript> Boxes => boxList;

    private float startTime = 0.0f;
    private float time = 0.0f;

    public float CompletionTime => time;

    private void Start()
    {
        time = 0.0f;
        startTime = Time.time;
        navMesh.BuildNavMesh();
        UpdateBoxList();
    }

    public void Update() {
        time = Time.time - startTime;
    }

    public void UpdateBoxList()
    {
        boxList = boxesParent.Cast<Transform>().Where(child => child != null).Select(child => child.GetComponent<BoxScript>()).ToList();
    }

    public BoxScript SpawnBox(Vector2 pos)
    {
        var box = Instantiate(boxPrefab, pos, Quaternion.identity, boxesParent);
        UpdateBoxList();
        return box;
    }

    public void MoveBox(BoxScript box, Transform newParent)
    {
        box.transform.parent = newParent;
        UpdateBoxList();
    }

    public void ReturnBox(BoxScript box)
    {
        box.transform.SetParent(boxesParent, true);
        UpdateBoxList();
    }

    public BoxScript FindNearestBox(Vector2 fromPos)
    {
        var boxes = Level.Current.Boxes;
        var minBox = boxes.FirstOrDefault();
        var minDis = float.MaxValue;

        foreach (var box in boxes)
        {
            if (box)
            {
                var vec = (Vector2)box.transform.position - fromPos;
                var dis = vec.sqrMagnitude;
                if (dis < minDis)
                {
                    minBox = box;
                    minDis = dis;
                }
            }
        }
        return minBox;
    }

    public bool IsBoxPickable(BoxScript box)
    {
        return box != null && box.transform.parent == boxesParent;
    }

}
