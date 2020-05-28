using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class DropOff : CompositeGoal
{
    private Boat _owner;
    private Vector3 _storagePosition;

    public DropOff(Boat boat)
    {
        _owner = boat;
        Name = "Drop off";
        _storagePosition = GameObject.FindGameObjectWithTag("Storage").transform.position;
    }

    public override void Activate()
    {
        Status = GoalStatus.Active;

        AddSubGoal(new MoveTo(_owner.gameObject, _storagePosition, 10f));
    }

    public override GoalStatus Process()
    {
        if (SubGoals.Any(subGoal => subGoal.Status == GoalStatus.Failed))
            Status = GoalStatus.Failed;

        return base.Process();
    }

    public override void Terminate()
    {
        Debug.Log("Terminating drop off!");
        foreach (KeyValuePair<string, float> resource in _owner.CarriedResources)
        {
            ResourceManager.Instance.IncreaseResource(resource.Key, (int)resource.Value);
            Debug.Log("Dropping off " + resource.Key);
        }
        _owner.ClearCarriedResources();

        Status = GoalStatus.Completed;
    }
}
