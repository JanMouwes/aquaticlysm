using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStateMachine : MonoBehaviour
{
    private IState _currentState;

    private void Start()
    {
        _currentState = new Play();
    }

    private void Update()
    {
        _currentState.Execute();
    }

    public void ChangeState(IState state) 
    {
        _currentState.Stop();
        _currentState = state;
        _currentState.Start();
    }
}
