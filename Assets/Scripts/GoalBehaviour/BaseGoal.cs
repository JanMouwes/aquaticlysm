using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for Goal Driven Behaviour
/// </summary>
public abstract class BaseGoal : MonoBehaviour
{
    public GameObject gameObject;

    // Status to keep a record of the status of the goal
    public GoalStatus GoalStatus { get; set; }

    // Helper to print goal name if needed
    public string goalName;

    public abstract void Activate();

    public abstract GoalStatus Process();

    public abstract void Terminate();
}

/// <summary>
/// For keeping track of the completion status of goals
/// </summary>
public enum GoalStatus
{ 
    Inactive,
    Active,
    Completed,
    Failed
}
