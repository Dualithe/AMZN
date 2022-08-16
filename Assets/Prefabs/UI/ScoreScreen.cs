using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ScoreScreen : MonoBehaviour
{
    [SerializeField] private List<StarIcon> stars;
    [SerializeField] private List<TMP_Text> timeRequiredTexts;
    [SerializeField] private TMP_Text completionTimeText;
    [SerializeField] private TMP_Text levelText;

    private void Start() {
        foreach (var star in stars) {
            star.Reset();
        }
    }

    private void OnEnable() {
        InputHandler.Instance.event_Pickup.started += PlayNextLevel;
    }

    private void OnDisable() {
        InputHandler.Instance.event_Pickup.started -= PlayNextLevel;
    }

    public void PlayNextLevel(InputAction.CallbackContext context) {
        LevelManager.Instance.ChangeToNextLevel();
    }

    public void SetupWindow(float completionTime, List<float> timesRequiredForStars, int levelId) {
        for (var i = 0; i < timesRequiredForStars.Count; ++i) {
            timeRequiredTexts[i].text = timesRequiredForStars[i].ToString("0.00s");
        }
        completionTimeText.text = completionTime.ToString("0.00s");
        levelText.text = $"Level {levelId + 1}";
    }

    public void PlayAnimation(int starsAmount) {

        foreach (var star in stars) {
            star.Reset();
        }

        for (int i = 0; i < starsAmount; ++i) {
            stars[i].PlayAnimation();
        }
    }

}
