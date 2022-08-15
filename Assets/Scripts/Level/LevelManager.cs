using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform levelHandler;
    [SerializeField] private List<Level> levelPrefabs;

    private Level currentLevel = null;
    public Level CurrentLevel => currentLevel;

    private static LevelManager instance;
    public static LevelManager Instance => instance;

    private void Start() {
        currentLevel = levelHandler.childCount > 0 ? levelHandler.GetChild(0).GetComponent<Level>() : null;
    }

    private void Awake() {
        if (instance != null) {
            instance = this;
        }
    }

    public void ChangeLevel(int levelId) {
        if (currentLevel != null) {
            Destroy(currentLevel.gameObject);
        }
        var levelPrefab = levelPrefabs[levelId];
        currentLevel = Instantiate(levelPrefab, levelHandler);
        currentLevel.transform.position = Vector3.zero;
    }
}
