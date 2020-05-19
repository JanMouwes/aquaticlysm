using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    GlobalState StateType();
    void Start();
    void Execute();
    void Stop();
}
