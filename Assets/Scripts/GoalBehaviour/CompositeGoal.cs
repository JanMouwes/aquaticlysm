using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Base class for composite goals constructed of multiple goals
/// </summary>
public abstract class CompositeGoal : IGoal
{
    public GoalStatus Status { get; set; }
    public string Name { get; set; }

    // List for holding all activated goals waiting to be met
    public Queue<IGoal> SubGoals { get; }
   
    public CompositeGoal()
    {
        SubGoals = new Queue<IGoal>();
    }

    public void AddSubGoal(IGoal goal)
    {
        SubGoals.Enqueue(goal);
    }    
    
    public void PrioritizeSubGoal(IGoal goal)
    {
        ClearAllGoals();
        SubGoals.Enqueue(goal);
    }
    
    public void ClearAllGoals()
    {
        if (SubGoals.Count > 0)
        {
            foreach (IGoal subGoal in SubGoals)
            {
                if (subGoal is CompositeGoal compositeGoal)
                    compositeGoal.ClearAllGoals();
            }

            SubGoals.Clear();
        }
    }

    public abstract void Activate();

    public abstract GoalStatus Process();

    public abstract void Terminate();

    /// <summary>
    /// Make sure the processed goal is not already completed or failed
    /// </summary>
    public void RemoveCompletedSubgoals()
    {
        if (SubGoals.Peek().Status == GoalStatus.Completed || SubGoals.Peek().Status == GoalStatus.Failed)
        {
            SubGoals.Dequeue();
        }
    }
}