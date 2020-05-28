using UnityEngine;
class Fish : IGoal
{
    private Boat _owner;
    private static float _maxCarrierAmount;
    private float _fishAmount;

    public GoalStatus Status { get; private set; }
    public string Name { get; private set; }

    public Fish(Boat boat)
    {
        _owner = boat;
        _maxCarrierAmount = boat.MaxCarrierAmount;
        _fishAmount = _owner.TryGetResourceValue("fish");
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
            if (IsFullOfResources(_owner.CountResourcesCarried(), _fishAmount))
                Terminate();
            else
                _fishAmount += 1.5f * Time.deltaTime;
        }

        return Status;
    }

    public void Terminate()
    {
        _owner.CarriedResources["fish"] = _fishAmount;
        Status = GoalStatus.Completed;
    }

    /// <summary>
    /// Check if the amount of fish is sufficient than a certain threshold.
    /// </summary>
    /// <param name="fishAmount">Amount of fish on the boat currently.</param>
    /// <returns>true or false</returns>
    private static bool IsFullOfResources(float carriedAmount, float currentlyAddedAmount)
    { 
        return carriedAmount + currentlyAddedAmount >= _maxCarrierAmount;
    }
}
