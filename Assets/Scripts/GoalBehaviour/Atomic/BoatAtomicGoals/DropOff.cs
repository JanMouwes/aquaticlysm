using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class DropOff : CompositeGoal
{
    private Boat _owner;
    private Vector3 _storagePosition;

    public GoalStatus Status { get; private set; }
    public string Name { get; private set; }

    public DropOff(Boat boat)
    {
        _owner = boat;
        Name = "Drop off";
        _storagePosition = GameObject.FindGameObjectWithTag("Storage").GetComponent<GameObject>().transform.position;
    }

    public override void Activate()
    {
        Status = GoalStatus.Active;

        AddSubGoal(new MoveTo(_owner.gameObject, _storagePosition));
    }

    public override GoalStatus Process()
    {
        if (SubGoals.Any(subGoal => subGoal.Status == GoalStatus.Failed))
            Status = GoalStatus.Failed;

        return Status;
    }

    public override void Terminate()
    {
        foreach (KeyValuePair<string, float> resource in _owner.CarriedResources)
        {
            ResourceManager.Instance.IncreaseResource(resource.Key, (int)resource.Value);
        }

        _owner.ClearCarriedResources();
        Status = GoalStatus.Completed;
    }
}
