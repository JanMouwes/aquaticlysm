using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : IGoal
{
    public Character Owner => throw new System.NotImplementedException();

    public GoalStatus Status => throw new System.NotImplementedException();

    public string Name => throw new System.NotImplementedException();

    MoveTo(Vector3 position)
    {
    
    }

    public void Activate()
    {
        throw new System.NotImplementedException();
    }

    public GoalStatus Process()
    {
        throw new System.NotImplementedException();
    }

    public void Terminate()
    {
        throw new System.NotImplementedException();
    }
}
