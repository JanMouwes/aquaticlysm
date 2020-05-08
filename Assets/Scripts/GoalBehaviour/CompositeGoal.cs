using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Base class for composite goals constructed of multiple goals
/// </summary>
public abstract class CompositeGoal : IBaseGoal
{
    public Character character;

    public string goalName;

    // List for holding all activated goals waiting to be met
    public Queue<IBaseGoal> subGoals { get; set; }
    public GoalStatus status { get; set; }

    public CompositeGoal(GameObject owner)
    {
        subGoals = new Queue<IBaseGoal>();
        character = owner.gameObject.GetComponent<Character>();
    }

    public void AddSubGoal(IBaseGoal goal)
    {
        subGoals.Enqueue(goal);
    }

    public abstract void Activate();

    public abstract GoalStatus Process();

    public abstract void Terminate();

    /// <summary>
    /// Make sure the processed goal is not already completed or failed
    /// </summary>
    public void CheckAndRemoveCompletedSubgoals()
    {
        if (subGoals.Peek().status == GoalStatus.Completed || subGoals.Peek().status == GoalStatus.Failed)
        {
            subGoals.Dequeue();
        }
    }

}