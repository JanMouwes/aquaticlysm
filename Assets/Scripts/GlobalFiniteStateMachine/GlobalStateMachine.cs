using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GlobalState { Play, Build}

public class GlobalStateMachine : MonoBehaviour
{
    public static GlobalStateMachine current;
    private IState _currentState;

    private void Start()
    {
        current = this;
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

    public bool IsState(GlobalState type) => (_currentState.StateType() == type) ? true : false ;
}
