using UnityEngine;

class Fish : IGoal
{
    private Boat _owner;
    private static float _maxCarrierAmount;
    public GoalStatus Status { get; private set; }
    public string Name { get; private set; }

    public Fish(Boat boat)
    {
        _owner = boat;
        _maxCarrierAmount = boat.MaxCarrierAmount;
        Name = "Fishing";
    }

    public void Activate()
    {
        Status = GoalStatus.Active;
    }

    public GoalStatus Process()
    {
        if (Status == GoalStatus.Inactive)
            Activate();

        if (Status == GoalStatus.Active)
        {
            if (IsFullOfFish(_owner.AmountOfFish))
                Terminate();
            else
                _owner.AmountOfFish += 1.5f * Time.deltaTime;
        }

        return Status;
    }

    public void Terminate()
    {
        Status = GoalStatus.Completed;
    }

    /// <summary>
    /// Check if the amount of fish is sufficient than a certain threshold.
    /// </summary>
    /// <param name="fishAmount">Amount of fish on the boat currently.</param>
    /// <returns>true or false</returns>
    private static bool IsFullOfFish(float fishAmount)
    { 
        return fishAmount >= _maxCarrierAmount;
    }
}
