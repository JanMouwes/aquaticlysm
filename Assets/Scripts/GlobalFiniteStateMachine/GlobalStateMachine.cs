using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStateMachine : MonoBehaviour
{
    public static GlobalStateMachine current;
    private IState _currentState;

    public event Action<IState> Statechanged;

    private void Start()
    {
        current = this;
        _currentState = new Play();
    }

    private void Update()
    {
        _currentState.Execute();

        if (Input.GetKeyDown(KeyCode.B))
        {
            ChangeState(new Build());
        }        
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangeState(new Play());
        }
    }

    public void ChangeState(IState state) 
    {
        _currentState.Stop();
        _currentState = state;
        _currentState.Start();
        Statechanged(_currentState);
    }
}
