using System.Linq;
using Buildings.Farm;
using Buildings.Farm.States;
using GoalBehaviour.Composite;

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
    public IGoal DetermineNewGoal()
    {
        Farm[] emptyFarms = Farm.FarmsWithState<Empty>()
                                .Where(farm => !farm.IsClaimed)
                                .ToArray();

        if (emptyFarms.Length > 0)
        {
            Farm farm = emptyFarms.First();

            return new SowSeedsAtFarm(this._owner, farm);
        }

        Farm[] fullFarms = Farm.FarmsWithState<FullGrown>()
                               .Where(farm => !farm.IsClaimed)
                               .ToArray();

        if (fullFarms.Length > 0)
        {
            Farm farm = fullFarms.First();

            return new HarvestCropsAtFarm(this._owner, farm);
        }


        // Check if there is need for resting and also that the agent is not currently taking care of it.
        if (needsRest(_owner.energyLevel)) 
        {
           NotificationSystem.Instance.ShowNotification("VillagerNeedsRest", 3);

           return new Rest(_owner);
        }

        return null;
    }

    public override GoalStatus Process()
    {
        if (Status == GoalStatus.Inactive)
            Activate();

        if (SubGoals.Count == 0)
        {
            IGoal newGoal = DetermineNewGoal();

            if (newGoal != null) { AddSubGoal(newGoal); }
        }

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