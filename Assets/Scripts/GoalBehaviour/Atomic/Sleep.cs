using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : IGoal
{
    public Character Owner { get; private set; }
    public GoalStatus Status { get; private set; }
    public string Name { get; private set; }

    public Sleep(Character owner) 
    {
        Owner = owner;
        Name = "Sleep";
    }

    public void Activate()
    {
        Status = GoalStatus.Active;
    }

    public GoalStatus Process()
    {
        if (Status == GoalStatus.Inactive)
            Activate();

        // Replenishes the energy level.
        if (!isRested(Owner.energyLevel))
            Owner.energyLevel += 20 * Time.deltaTime;
        else
            Terminate();

        return Status;
    }

    public void Terminate()
    {
        Status = GoalStatus.Completed;
    }

    /// <summary>
    ///     Check, if energylevel has been met or not.
    /// </summary>
    /// <param name="energyLevel">Amount of energy.</param>
    /// <returns>true or false</returns>
    private static bool isRested(float energyLevel) => energyLevel >= 100.0f;
}
