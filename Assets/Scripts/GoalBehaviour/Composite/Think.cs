using UnityEngine;

/// <summary>
///     Highest level of goal managing, makes decisions between strategies to
///     fulfill agents pressing needs.
/// </summary>
public class Think : CompositeGoal
{
    private Character _owner;
    
    public Think(Character owner)
    {
        Name = "Think";
        this._owner = owner;
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
        // Check if there is need for resting and also that the agent is not currently taking care of it.
        if (needsRest(_owner.energyLevel))
        {
            Vector3 restLocation = GameObject.FindGameObjectWithTag("Rest").transform.position;
            AddSubGoal(new Rest(_owner));
        }
    }

    public override GoalStatus Process()
    {
        if (Status == GoalStatus.Inactive)
            Activate();

        if (SubGoals.Count == 0)
        {
            Evaluate();
        }
        else
        {
            // Process new subgoal
            SubGoals.Peek().Process();
            RemoveCompletedSubgoals();
        }

        return Status;
    }

    public override void Terminate()
    {
    }

    /// <summary>
    /// Check, if an entity needs rest.
    /// </summary>
    /// <param name="energyLevel">Amount of energy.</param>
    /// <returns>true or false</returns>
    private static bool needsRest(float energyLevel) => energyLevel < 10;
}
