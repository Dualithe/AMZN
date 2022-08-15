using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform levelHandler;
    [SerializeField] private List<Level> levelPrefabs;

    private Level currentLevel = null;
    public Level CurrentLevel => currentLevel;

    private static LevelManager instance;
    public static LevelManager Instance => instance;

    [Space]
    [SerializeField] private TMP_Text currentLevelText;

    private void UpdateHud() {
        currentLevelText.gameObject.SetActive(currentLevel != null);
        if (currentLevel != null) {
            currentLevelText.text = $"Level ${1}";
        }
    }

    public void Start() {
        UpdateHud();
    }

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        currentLevel = levelHandler.childCount > 0 ? levelHandler.GetChild(0).GetComponent<Level>() : null;
    }

    public void ChangeLevel(int levelId) {
        if (currentLevel != null) {
            Destroy(currentLevel.gameObject);
        }
        var levelPrefab = levelPrefabs[levelId];
        currentLevel = Instantiate(levelPrefab, levelHandler);
        currentLevel.transform.position = Vector3.zero;
        UpdateHud();
    }
}
