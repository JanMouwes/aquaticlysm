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
        Debug.Log(Name);
        if (Status == GoalStatus.Inactive)
            Activate();

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
    /// <param name="energyLevel"></param>
    /// <returns></returns>
    private static bool isRested(float energyLevel)
    {
        return energyLevel >= 100.0f;
    }
}
