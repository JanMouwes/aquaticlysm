using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;

public class EventState : IState
{
    readonly EventStateHandeler _handeler = ScriptableObject.CreateInstance<EventStateHandeler>();
    
    public void Start()
    {
        _handeler.EnterState();
    }

    public void Execute()
    {
        
    }

    public void Stop()
    {
        _handeler.ExitState();
    }
}
