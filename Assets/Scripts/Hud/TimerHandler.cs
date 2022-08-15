using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimerHandler : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI timer;
    private float savedTime = 0;
    private bool running;

    private void Start()
    {
        passAndResetTime();
        startTimer();
    }

    private void Update()
    {
        if (running)
        {
            timer.text = Time.time.ToString("0.##");
        }
    }

    public void passAndResetTime()
    {
        savedTime = float.Parse(timer.text);
        timer.text = 0.ToString();
    }

    public void stopTimer()
    {
        running = false;
    }

    public void startTimer()
    {
        running = true;
    }
}
