using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Base class for composite goals constructed of multiple goals
/// </summary>
public abstract class CompositeGoal : IGoal
{
    public Character Owner { get; set; }
    public GoalStatus Status { get; set; }
    public string Name { get; set; }

    // List for holding all activated goals waiting to be met
    public Queue<IGoal> subGoals { get;}
   
    public CompositeGoal(Character owner)
    {
        subGoals = new Queue<IGoal>();
        Owner = owner;
    }

    public void AddSubGoal(IGoal goal)
    {
        subGoals.Enqueue(goal);
    }    
    
    public void PrioritizeSubGoal(IGoal goal)
    {
        ClearAllGoals();
        subGoals.Enqueue(goal);
    }
    
    public void ClearAllGoals()
    {
        if (subGoals.Count > 0)
        {
            foreach (IGoal subGoal in subGoals)
            {
                if (subGoal is CompositeGoal compositeGoal)
                    compositeGoal.ClearAllGoals();
            }

            subGoals.Clear();
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
        if (subGoals.Peek().Status == GoalStatus.Completed || subGoals.Peek().Status == GoalStatus.Failed)
        {
            subGoals.Dequeue();
        }
    }
}