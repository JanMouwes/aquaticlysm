using Resources;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class DropOff : CompositeGoal
{
    private Boat _owner;
    private Vector3 _storagePosition;

    public DropOff(Boat boat)
    {
        Name = "Drop off";
        _owner = boat;
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
        ResourceManager resourceManager = Object.FindObjectOfType<ResourceManager>();
        // Add the fetched resources to the resource manager
        foreach (KeyValuePair<string, float> resource in _owner.carriedResources)
            resourceManager.IncreaseResource(resource.Key, (int)resource.Value);
        
        // Clear the carried resources
        _owner.ClearCarriedResources();

        Status = GoalStatus.Completed;
    }
}
