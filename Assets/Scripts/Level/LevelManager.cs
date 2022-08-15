using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform levelHandler;
    [SerializeField] private Transform mainMenu;
    [SerializeField] private Transform hud;
    [SerializeField] private List<Level> levelPrefabs;

    private Level currentLevel = null;
    public Level CurrentLevel => currentLevel;

    private static LevelManager instance;
    public static LevelManager Instance => instance;

    [Space]
    [SerializeField] private TMP_Text currentLevelText;

    private void UpdateUI() {
        currentLevelText.gameObject.SetActive(currentLevel != null);
        if (currentLevel != null) {
            currentLevelText.text = $"Level ${1}";
        }
        mainMenu.gameObject.SetActive(currentLevel == null);
        hud.gameObject.SetActive(currentLevel != null);
    }

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        currentLevel = levelHandler.childCount > 0 ? levelHandler.GetChild(0).GetComponent<Level>() : null;
    }

    public void Start() {
        UpdateUI();
    }

    public void CloseLevel() {
        if (currentLevel != null) {
            Destroy(currentLevel.gameObject);
        }
        currentLevel = null;
        mainMenu.gameObject.SetActive(true);
        hud.gameObject.SetActive(false);
        UpdateUI();
    }

    public void ChangeLevel(int levelId) {
        if (currentLevel != null) {
            Destroy(currentLevel.gameObject);
        }
        var levelPrefab = levelPrefabs[levelId];
        currentLevel = Instantiate(levelPrefab, levelHandler);
        currentLevel.transform.position = Vector3.zero;
        UpdateUI();
    }
}
