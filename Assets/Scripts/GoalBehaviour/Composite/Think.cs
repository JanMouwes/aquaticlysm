using UnityEngine;

/// <summary>
///     Highest level of goal managing, makes decisions between strategies to
///     fulfill agents pressing needs.
/// </summary>
public class Think : CompositeGoal
{
    public Think(Character owner) : base(owner)
    {
        Name = "Think";
    }

    public override void Activate()
    {
        Status = GoalStatus.Active;
    }

    /// <summary>
    ///     Checks if there is a need for a new goal.
    /// </summary>
    public void Evaluate()
    {
        // Check if there is need for resting and also that the agent is not currently taking care of it
        if (Owner.energyLevel < 10)
        {
            Vector3 restLocation = GameObject.FindGameObjectWithTag("Rest").transform.position;
            AddSubGoal(new Rest(Owner, restLocation));
        }
    }

    public override GoalStatus Process()
    {
        // Activate, if goalstatus not yet active
        if (Status == GoalStatus.Inactive)
            Activate();

        if (subGoals.Count == 0)
        {
            Evaluate();
        }
        else
        {
            // Process new subgoal
            subGoals.Peek().Process();
            Debug.Log(subGoals.Count);
            Debug.Log(subGoals.Peek().Status);
            CheckAndRemoveCompletedSubgoals();
        }

        return Status;
    }

    public override void Terminate()
    {
    }
}