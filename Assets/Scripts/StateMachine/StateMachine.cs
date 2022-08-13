using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine {

    public StateBase currentState;

    public void ChangeState(StateBase nextState) {
        currentState?.OnStateExited();
        currentState = nextState;
        currentState?.OnStateEntered();
    }

    public void UpdateState() {
        currentState?.OnStateUpdate();
    }
}