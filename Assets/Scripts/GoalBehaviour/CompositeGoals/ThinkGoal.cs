using UnityEngine;

/// <summary>
///     Highest level of goal managing, makes decisions between strategies to
///     fulfill agents pressing needs.
/// </summary>
public class ThinkGoal : CompositeGoal
{
    public ThinkGoal(Character owner) : base(owner)
    {
        Name = "Think";
        // To be continued...
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
            RestGoal restGoal = new RestGoal(Owner);
            AddSubGoal(restGoal);
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
            CheckAndRemoveCompletedSubgoals();
            // Process new subgoal
            subGoals.Peek().Process();
        }

        return Status;
    }

    public override void Terminate()
    {
    }
}