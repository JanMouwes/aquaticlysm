using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    /// <summary>
    /// Get's called when you enter a state.
    /// </summary>
    void Start();

    /// <summary>
    /// Get's called every update, and if it's active.
    /// </summary>
    void Execute();
    
    /// <summary>
    /// Get's called when you exit a state.
    /// </summary>
    void Stop();
}
