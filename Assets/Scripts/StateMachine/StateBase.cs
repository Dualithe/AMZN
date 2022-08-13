using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBase {
    public virtual void OnStateEntered() {}
    public virtual void OnStateUpdate() {}
    public virtual void OnStateExited() {}
}