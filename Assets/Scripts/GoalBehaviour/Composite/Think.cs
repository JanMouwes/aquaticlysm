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
    public void FindSubGoal()
    {
        // Check if there is need for resting and also that the agent is not currently taking care of it.
        if (needsRest(_owner.energyLevel)) { AddSubGoal(new Rest(_owner)); }
    }

    public override GoalStatus Process()
    {
        if (Status == GoalStatus.Inactive)
            Activate();

        if (SubGoals.Count == 0) { FindSubGoal(); }

        return base.Process();
    }

    public override void Terminate() { }

    /// <summary>
    /// Check, if an entity needs rest.
    /// </summary>
    /// <param name="energyLevel">Amount of energy.</param>
    /// <returns>true or false</returns>
    private static bool needsRest(float energyLevel) => energyLevel < 10;
}