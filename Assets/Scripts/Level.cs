using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class Level : MonoBehaviour
{
    private static Level instance;
    public static Level Current => instance;

    [SerializeField] private PlayerMovement player;
    [SerializeField] private BoxScript boxPrefab;
    [SerializeField] private Transform boxesParent;

    private List<BoxScript> boxList = new();

    public PlayerMovement Player => instance.player;
    public IReadOnlyList<BoxScript> Boxes => boxList;

    private void Awake() {
        instance = this;
    }

    public void UpdateBoxList() {
        boxList = boxesParent.Cast<Transform>().Where(child => child != null).Select(child => child.GetComponent<BoxScript>()).ToList();
    }

    public BoxScript SpawnBox(Vector2 pos) {
        var box = Instantiate(boxPrefab, pos, Quaternion.identity, boxesParent);
        UpdateBoxList();
        return box;
    }
    
    public void MoveBox(BoxScript box, Transform newParent) {
        box.transform.parent = newParent;
        UpdateBoxList();
    }

    public void ReturnBox(BoxScript box) {
        box.transform.SetParent(boxesParent, true);
        UpdateBoxList();
    }

    public BoxScript FindNearestBox(Vector2 fromPos) {
        var boxes = Level.Current.Boxes;
        var minBox = boxes.FirstOrDefault();
        var minDis = float.MaxValue;

        foreach (var box in boxes) {
            if (box) {
                var vec = (Vector2)box.transform.position - fromPos;
                var dis = vec.sqrMagnitude;
                if (dis < minDis) {
                    minBox = box;
                    minDis = dis;
                }
            }
        }
        return minBox;
    }

    public bool IsBoxPickable(BoxScript box) {
        return box != null && box.transform.parent == boxesParent;
    }

}
