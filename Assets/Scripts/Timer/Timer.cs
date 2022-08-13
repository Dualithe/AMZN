using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer {
    
    private float time = 0.0f;
    private event Action callback;

    private float waitTime = 0.0f;
    public float WaitTime { 
        set => waitTime = Math.Max(value, 0.0f); 
        get => waitTime; 
    }
    
    public float TimeLeft => Math.Min(time, waitTime);

    private bool isRunning = false;
    public bool IsRunning => isRunning;

    public bool Oneshot { set; get; } = true;

    public void Resume() {
        isRunning = true;
    }

    public void Start() {
        isRunning = true;
        time = 0.0f;
    }

    public void Stop() {
        isRunning = false;
    }

    public void AddCallback(Action action) {
        callback += action;
    }

    public void Update(float dt) {
        if (isRunning) {
            time += dt;
            while (time > waitTime) {
                time -= waitTime;
                callback?.Invoke();
                if (Oneshot) {
                    isRunning = false;
                    break;
                }
            }
        }
    }

    public static void StartOneshotTimer(System.Object target, float waitTime, Action callback) {
        var timer = new Timer();
        timer.WaitTime = waitTime;
        timer.AddCallback(callback);
        timer.Oneshot = true;
        timer.Start();
        TimerManager.Instance.AddTimer(timer, target, true);
    }

    public static Timer CreateTimer(System.Object target, float waitTime, Action callback) {
        var timer = new Timer();
        timer.WaitTime = waitTime;
        timer.AddCallback(callback);
        timer.Oneshot = false;
        TimerManager.Instance.AddTimer(timer, target, false);
        return timer;
    }

    public static Timer StartTimer(System.Object target, float waitTime, Action callback) {
        var timer = CreateTimer(target, waitTime, callback);
        timer.Start();
        return timer;
    }

    private LinkedListNode<(Timer timer, System.Object target, bool destroyIfFinished)> timerNode = null;

    ~Timer() {
        if (timerNode != null) {
            TimerManager.Instance.RemoveTimer(timerNode);
        }
        else {
            TimerManager.Instance.RemoveTimer(this);
        }
    }
}