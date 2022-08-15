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

    void Start() {
        text.text = (levelId + 1).ToString();
        GetComponent<Button>().onClick.AddListener(() => LevelManager.Instance.ChangeLevel(levelId));
    }
}
