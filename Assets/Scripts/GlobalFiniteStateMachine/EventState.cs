using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;

public class EventState : IState
{
    readonly EventStateHandeler _handler = ScriptableObject.CreateInstance<EventStateHandeler>();
    
    public void Start()
    {
        _handler.EnterState();
    }

    public void Execute()
    {
        
    }

    public void Stop()
    {
        _handler.ExitState();
    }
}
