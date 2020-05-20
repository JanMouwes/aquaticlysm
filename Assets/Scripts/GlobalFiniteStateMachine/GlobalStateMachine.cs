using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStateMachine : MonoBehaviour
{
    public static GlobalStateMachine instance;
    public event Action<IState> StateChanged;

    private IState _currentState;

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


    /// <summary>
    /// Changes the state and invokes all observers.
    /// </summary>
    /// <param name="state">The desired state.</param>
    public void ChangeState(IState state) 
    {
        _currentState.Stop();
        _currentState = state;
        _currentState.Start();

        StateChanged?.Invoke(_currentState);
    }
}
