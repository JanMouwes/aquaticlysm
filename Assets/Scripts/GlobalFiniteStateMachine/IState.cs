using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    /// <summary>
    /// Gets called when you enter a state.
    /// </summary>
    void Start();

    /// <summary>
    /// Gets called every update, and if it's active.
    /// </summary>
    void Execute();
    
    /// <summary>
    /// Gets called when you exit a state.
    /// </summary>
    void Stop();
}
