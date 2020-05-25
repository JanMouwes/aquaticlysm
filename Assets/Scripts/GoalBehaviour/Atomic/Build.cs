using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : IGoal
{
    public Character Owner { get; private set; }
    public GoalStatus Status { get; private set; }
    public string Name { get; private set; }

    private DissolveController _dissolveController;

    public Build(Character owner, GameObject gameObject) 
    {
        Owner = owner;
        Name  = "Construct";
        _dissolveController = gameObject.GetComponent<DissolveController>();
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
        if (isExhausted(Owner.energyLevel))
            Status = GoalStatus.Failed;
        
        // Build
        if(Status == GoalStatus.Active)
            if (_dissolveController.Build(0.1f * Time.deltaTime))
                Terminate();
        
        return Status;
    }

    public void Terminate()
    {
        Status = GoalStatus.Completed;
    }

    /// <summary>
    /// Check if energy level is lower than a certain threshold.
    /// </summary>
    /// <param name="energyLevel">Amount of energy.</param>
    /// <returns>true or false</returns>
    private static bool isExhausted(float energyLevel) => energyLevel <= 10.0f;
}
