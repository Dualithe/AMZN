using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimerManager : MonoBehaviour
{
    private LinkedList<(Timer timer, System.Object target, bool destroyIfFinished)> timers = new();

    private static TimerManager instance;
    public static TimerManager Instance => instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    public void RemoveTimer(LinkedListNode<(Timer timer, System.Object target, bool destroyIfFinished)> timerNode) {
        timers.Remove(timerNode);
    }

    public void RemoveTimer(Timer timer) {
        var itr = timers.First;
        if (itr != null) {
            if (itr.Value.timer == timer) {
                timers.Remove(itr);
                return;
            }
            itr = itr.Next;
        }
    }

    public LinkedListNode<(Timer timer, System.Object target, bool destroyIfFinished)> AddTimer(Timer timer, System.Object target, bool destroyIfFinished) {
        timers.AddLast((timer, target, destroyIfFinished));
        return timers.Last;
    }

    private void Update() {
        var timerItr = timers.First;
        while (timerItr != null) {
            var next = timerItr.Next;
            var (timer, target, destroy) = timerItr.Value;
            if (target != null) {
                timer.Update(Time.deltaTime);
            }
            else {
                timers.Remove(timerItr);            
                timerItr = next;
                continue;
            }
            if (destroy && !timer.IsRunning) {
                timers.Remove(timerItr);
            }
            timerItr = next;
        }
    }

}
