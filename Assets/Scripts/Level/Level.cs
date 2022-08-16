using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
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

    private void OnEnable() {
        InputHandler.Instance.event_BackToMenu.started += _BackToMenu;
    }

    private void OnDisable() {
        InputHandler.Instance.event_BackToMenu.started -= _BackToMenu;
    }

    private void _BackToMenu(InputAction.CallbackContext context) {
        LevelManager.Instance.ChangeToMainMenu();
    }

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

    public BoxScript FindNearestBox(Vector2 fromPos, List<BoxScript> excludedBoxes = null)
    {
        var boxes = Boxes;
        BoxScript minBox = null;
        var minDis = float.MaxValue;

        excludedBoxes = excludedBoxes ?? new List<BoxScript>();

        foreach (var box in boxes)
        {
            if (box != null && !excludedBoxes.Contains(box))
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

    public BoxScript FindRandomBox(Vector2 fromPos, List<BoxScript> excludedBoxes = null)
    {
        var boxes = Boxes;
        BoxScript minBox = null;

        excludedBoxes = excludedBoxes ?? new List<BoxScript>();

        var sortedByDistance = boxes.Except(excludedBoxes).Where(box => box != null).Select(box => 
            (box, ((Vector2)box.transform.position - fromPos).sqrMagnitude)
        ).OrderBy(pair => pair.sqrMagnitude).ToList();

        var range = System.Math.Min(sortedByDistance.Count, 5);
        if (range > 0) {
            minBox = sortedByDistance[Random.Range(0, range)].box;
        }

        return minBox;
    }

    public GameObject FindPressurePlate(Vector2 fromPos, List<GameObject> excludedPlates = null)
    {
        var plates = GameObject.FindGameObjectsWithTag("PressurePlate");
        GameObject minPlate = null;
        var minDis = float.MaxValue;

        excludedPlates = excludedPlates ?? new List<GameObject>();

        foreach (var plate in plates)
        {
            if (plate != null && !excludedPlates.Contains(plate))
            {
                var vec = (Vector2)plate.transform.position - fromPos;
                var dis = vec.sqrMagnitude;
                if (dis < minDis)
                {
                    minPlate = plate;
                    minDis = dis;
                }
            }
        }
        return minPlate;
    }

    public bool IsBoxPickable(BoxScript box)
    {
        return box != null && box.transform.parent == boxesParent;
    }

}
