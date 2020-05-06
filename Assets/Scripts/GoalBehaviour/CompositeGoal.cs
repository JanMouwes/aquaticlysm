﻿using System.Collections.Generic;

/// <summary>
///     Base class for composite goals constructed of multiple goals
/// </summary>
public abstract class CompositeGoal : BaseGoal
{
    // List for holding all activated goals waiting to be met
    public List<BaseGoal> subGoals { get; set; }

    public CompositeGoal()
    {
        subGoals = new List<BaseGoal>();
    }

    public void AddSubGoal(BaseGoal goal)
    {
        subGoals.Add(goal);
    }
}