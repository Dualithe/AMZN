using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform levelHandler;
    [SerializeField] private Transform mainMenu;
    [SerializeField] private Transform hud;
    [SerializeField] private ScoreScreen scoreScreen;
    [SerializeField] private List<Level> levelPrefabs;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text currentLevelText;
    [SerializeField] private List<LevelButton> levelButtons;
    [Space]

    private Level currentLevel = null;
    public Level CurrentLevel => currentLevel;
    private int currentLevelId = -1;

    private enum WindowType {
        Level, MainMenu, ScoreScreen
    }

    private WindowType currentWindow = 0;
    private float completionTime = 0.0f;

    private static LevelManager instance;
    public static LevelManager Instance => instance;

    private List<int> currentScoresForLevels = new();

    private void UpdateUI() {
        currentLevelText.gameObject.SetActive(currentLevel != null);
        if (currentWindow == WindowType.Level) {
            currentLevelText.text = $"Level {currentLevelId + 1}";
        }
        else if (currentWindow == WindowType.MainMenu) {
            for (int i = 0; i < levelButtons.Count; ++i) {
                levelButtons[i].SetupStars(currentScoresForLevels[i]);
            }
        }
        hud.gameObject.SetActive(currentWindow == WindowType.Level);
        mainMenu.gameObject.SetActive(currentWindow == WindowType.MainMenu);
        scoreScreen.gameObject.SetActive(currentWindow == WindowType.ScoreScreen);
    }

    private void Update() {
        if (currentLevel != null) {
            timerText.text = currentLevel.CompletionTime.ToString("0.00s");
        }
    }

    private void Awake() {
        currentScoresForLevels = levelPrefabs.Select(_ => -1).ToList();
        if (instance == null) {
            instance = this;
        }
        currentLevel = levelHandler.childCount > 0 ? levelHandler.GetChild(0).GetComponent<Level>() : null;
        currentLevelId = levelHandler.childCount > 0 ? 0 : -1;
        currentWindow = CurrentLevel != null ? WindowType.Level : WindowType.MainMenu;
    }

    public void Start() {
        UpdateUI();
    }

    public void ChangeToMainMenu() {
        TimerManager.Instance.RemoveAllTimers();
        if (currentLevel != null) {
            Destroy(currentLevel.gameObject);
        }
        currentLevel = null;
        currentWindow = WindowType.MainMenu;
        UpdateUI();
    }

    public void ChangeToScore() {
        TimerManager.Instance.RemoveAllTimers();
        currentWindow = WindowType.ScoreScreen;
        UpdateUI();

        if (currentLevel == null) {
            return;
        }
        
        scoreScreen.SetupWindow(completionTime, currentLevel.timeRequiredForStars, currentLevelId);

        var starsEarned = 0;
        foreach (var time in currentLevel.timeRequiredForStars) {
            if (completionTime < time) {
                ++starsEarned;
            }
            else {
                break;
            }
        }

        currentScoresForLevels[currentLevelId] = starsEarned;

        // Debug.Log(starsEarned);

        scoreScreen.PlayAnimation(starsEarned);

        Destroy(currentLevel.gameObject);

        currentLevel = null;
    }

    public void ChangeLevel(int levelId) {
        TimerManager.Instance.RemoveAllTimers();
        if (currentLevel != null) {
            Destroy(currentLevel.gameObject);
        }
        var levelPrefab = levelPrefabs[levelId];
        currentLevelId = levelId;
        currentLevel = Instantiate(levelPrefab, levelHandler);
        currentLevel.transform.position = Vector3.zero;
        currentWindow = WindowType.Level;
        UpdateUI();
    }

    public void ChangeToNextLevel() {
        TimerManager.Instance.RemoveAllTimers();
        var nextLevel = currentLevelId + 1;
        if (nextLevel >= 0 && nextLevel < levelPrefabs.Count) {
            ChangeLevel(nextLevel);
        }
        else {
            ChangeToMainMenu();
        }
    }

    public void CheckWinCondition() {
        var allShelfs = GameObject.FindGameObjectsWithTag("ShelfModule").Select(shelf => shelf.GetComponent<ShelfModule>());
        foreach (var shelf in allShelfs) {
            if (!shelf.IsFilled) {
                return;
            }
        }
        completionTime = currentLevel.CompletionTime;
        ChangeToScore();
    }
}
