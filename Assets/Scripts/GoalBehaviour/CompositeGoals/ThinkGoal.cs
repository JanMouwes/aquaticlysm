using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Highest level of goal managing, makes decisions between strategies to
/// fulfill agents pressing needs.
/// </summary>
public class ThinkGoal : CompositeGoal
{

    public ThinkGoal(GameObject owner) : base(owner)
    {
        goalName = "Think";
        // To be continued...
    }

    public override void Activate()
    {
        status = GoalStatus.Active;
    }

    /// <summary>
    /// Checks if there is a need for a new goal.
    /// </summary>
    public void Evaluate()
    {
        // Check if there is need for resting and also that the agent is not currently taking care of it
        if (character.energyLevel < 10)
        {
            RestGoal restGoal = new RestGoal(character.gameObject);
            AddSubGoal(restGoal);
        }
    }

    public override GoalStatus Process()
    {
        Debug.Log(goalName);

        // Activate, if goalstatus not yet active
        if (status == GoalStatus.Inactive)
            Activate();

        if (subGoals.Count == 0)
            Evaluate();
        else
        {
            CheckAndRemoveCompletedSubgoals();
            // Process new subgoal
             subGoals.Peek().Process();
        }

        return status;
    }

    public override void Terminate()
    {
    }
}
