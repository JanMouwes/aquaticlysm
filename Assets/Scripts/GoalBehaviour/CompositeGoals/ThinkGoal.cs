using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Highest level of goal managing, makes decisions between strategies to
/// fulfill agents pressing needs.
/// </summary>
public class Think : CompositeGoal
{
    public Think(GameObject gameObject) : base(gameObject)
    {
        goalName = "Think";
        // To be continued...
    }

    public override void Activate()
    {
        GoalStatus = GoalStatus.Active;
    }

    public override GoalStatus Process()
    {
        // Activate, if goalstatus not yet active
        if (GoalStatus == GoalStatus.Inactive)
            Activate();

        // Make sure all completed and failed subgoals are removed from the subgoal list
        subGoals.RemoveAll(sg => sg.GoalStatus == GoalStatus.Completed || sg.GoalStatus == GoalStatus.Failed);

        // Process new subgoals
        subGoals.ForEach(sg => sg.Process());

        return GoalStatus;
    }

    public override void Terminate()
    {
        throw new System.NotImplementedException();
    }
}
