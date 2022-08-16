using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButton : MonoBehaviour
{   
    [SerializeField] private int levelId;
    [Space]
    // [SerializeField] private Button button;
    [SerializeField] private TMP_Text text;

    [SerializeField] private List<Transform> starBacks;
    [SerializeField] private List<Transform> starIcons;

    void Start() {
        text.text = (levelId + 1).ToString();
        GetComponent<Button>().onClick.AddListener(() => LevelManager.Instance.ChangeLevel(levelId));
    }

    public void SetupStars(int amount = -1) {
        if (amount < 0) {
            foreach (var star in starBacks) {
                star.gameObject.SetActive(false);
            }
        }
        else {
            for (int i = 0; i < 3; ++i) {
                starBacks[i].gameObject.SetActive(true);
                starIcons[i].gameObject.SetActive(i < amount);
            }
        }
    }
}
