using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : IGoal
{
    public Character Owner { get; private set; }
    public GoalStatus Status { get; private set; }
    public string Name { get; private set; }
    private Vector3 _restPlace;

    public Sleep(Character owner , Vector3 restplace) 
    {
        Owner = owner;
        Name = "Sleep";
        _restPlace = restplace;
    }

    public void Activate()
    {
        Status = GoalStatus.Active;
        Owner.gameObject.GetComponent<Selectable>().Selected = false;
        Owner.agent.enabled = false;
        Owner.transform.position = new Vector3(10000,10000,10000);
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
        Owner.transform.position = _restPlace;
        Owner.agent.enabled = true;
        Status = GoalStatus.Completed;
    }

    /// <summary>
    ///     Check, if energylevel has been met or not.
    /// </summary>
    /// <param name="energyLevel">Amount of energy.</param>
    /// <returns>true or false</returns>
    private static bool isRested(float energyLevel) => energyLevel >= 100.0f;
}
