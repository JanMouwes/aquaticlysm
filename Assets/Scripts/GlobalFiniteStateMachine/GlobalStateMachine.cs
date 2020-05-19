using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStateMachine : MonoBehaviour
{
    public static GlobalStateMachine instance;
    private IState _currentState;
    public event Action<IState> StateChanged;
    private void Awake()
    {
        instance = this;
        _currentState = new Load();
    }

    private void Start()
    {
        ChangeState(new Play());
    }

    private void Update()
    {
        _currentState.Execute();

        if (Input.GetKeyDown(KeyCode.B))
        {
            ChangeState(new Build());
            Debug.Log("Build");
        }        
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangeState(new Play());
            Debug.Log("Play");
        }
    }

    public void ChangeState(IState state) 
    {
        _currentState.Stop();
        _currentState = state;
        _currentState.Start();

        StateChanged?.Invoke(_currentState);
    }
}
