using UnityEngine;

/// <summary>
///     Base class for Goal Driven Behaviour
/// </summary>
public abstract class BaseGoal : MonoBehaviour
{
    // Status to keep a record of the status of the goal
    public GoalStatus goalStatus { get; set; }

    // Helper to print goal name if needed
    public string goalName;

    public BaseGoal()
    {
        goalStatus = GoalStatus.Inactive;
    }

    public abstract void Activate();

    public abstract GoalStatus Process();

    public abstract void Terminate();
}

/// <summary>
///     For keeping track of the completion status of goals
/// </summary>
public enum GoalStatus
{
    Inactive,
    Active,
    Completed,
    Failed
}