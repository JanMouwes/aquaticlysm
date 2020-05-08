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
    public GoalStatus goalStatus { get; set; }

    public CompositeGoal(GameObject owner)
    {
        subGoals = new Queue<IBaseGoal>();
        character = owner.gameObject.GetComponent<Character>();
    }

    public void AddSubGoal(IBaseGoal goal)
    {
        subGoals.Enqueue(goal);
    }

    public virtual void Activate()
    {
        throw new System.NotImplementedException();
    }

    public virtual GoalStatus Process()
    {
        throw new System.NotImplementedException();
    }

    public virtual void Terminate()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Make sure the processed goal is not already completed or failed
    /// </summary>
    public void CheckAndRemoveCompletedSubgoals()
    {
        if (subGoals.Peek().goalStatus == GoalStatus.Completed || subGoals.Peek().goalStatus == GoalStatus.Failed)
        {
            subGoals.Dequeue();
        }
    }

}