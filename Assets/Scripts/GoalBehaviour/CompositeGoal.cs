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
        ClearGoals();
        SubGoals.Enqueue(goal);
    }

    public void ClearGoals()
    {
        if (SubGoals.Count > 0)
        {
            foreach (IGoal subGoal in SubGoals) { subGoal.Terminate(); }
            SubGoals.Clear();
        }

        Terminate();
    }

    public abstract void Activate();

    public virtual GoalStatus Process()
    {
        if (this.Status == GoalStatus.Failed)
            return GoalStatus.Failed;

        this.Status = ProcessSubgoals(SubGoals);

        return this.Status;
    }

    public abstract void Terminate();

    /// <summary>
    /// Processes goal queue
    /// </summary>
    /// <param name="subGoals">Goal queue to process</param>
    /// <returns>New goal status</returns>
    public static GoalStatus ProcessSubgoals(Queue<IGoal> subGoals)
    {
        RemoveCompletedSubgoals(subGoals);

        if (subGoals.Count == 0) 
            return GoalStatus.Completed;

        IGoal current = subGoals.Peek();

        if (current.Status == GoalStatus.Inactive)
            current.Activate();

        if (current.Status == GoalStatus.Active)
            current.Process();

        return GoalStatus.Active;
    }

    /// <summary>
    /// Make sure the processed goal is not already completed or failed
    /// </summary>
    /// <param name="subGoals">Queue from which to remove completed goals</param>
    public static void RemoveCompletedSubgoals(Queue<IGoal> subGoals)
    {
        while (subGoals.Count > 0 && (subGoals.Peek().Status == GoalStatus.Completed || subGoals.Peek().Status == GoalStatus.Failed))
            subGoals.Dequeue();
    }
}