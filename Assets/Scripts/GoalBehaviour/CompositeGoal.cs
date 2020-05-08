using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Base class for composite goals constructed of multiple goals
/// </summary>
public class CompositeGoal : BaseGoal
{
    public Character character;

    public string goalName;

    // For holding the currentGoal
    public BaseGoal currentGoal;

    // List for holding all activated goals waiting to be met
    public Queue<BaseGoal> subGoals { get; set; }
    public GoalStatus goalStatus { get; set; }


    public CompositeGoal(GameObject owner)
    {
        subGoals = new Queue<BaseGoal>();
        character = owner.gameObject.GetComponent<Character>();
    }

    public void AddSubGoal(BaseGoal goal)
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
        if (currentGoal.goalStatus == GoalStatus.Completed || currentGoal.goalStatus == GoalStatus.Failed)
        {
            if(subGoals.Count >= 1)
                currentGoal = subGoals.Dequeue();
            else
                currentGoal = null;
        }
    }
}