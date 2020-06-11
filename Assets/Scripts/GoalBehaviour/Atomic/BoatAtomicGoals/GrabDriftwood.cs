using Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabDriftwood : IGoal
{
    private Boat _owner;
    private GameObject _driftwood;
    private float _time;
    private int _accumulateAmount;

    public GoalStatus Status { get; private set; }
    public string Name { get; private set; }


    public GrabDriftwood(Boat owner, GameObject gameObject, float duration)
    {
        Name = "Grab driftwood";
        _owner = owner;
        _time = duration;
        _driftwood = gameObject;
    }

    public void Activate()
    {
        _accumulateAmount = 5;
        Status = GoalStatus.Active;
    }

    public GoalStatus Process()
    {
        if (Status == GoalStatus.Inactive)
            Activate();

        _time -= Time.deltaTime;

        if (_time <= 0)
            Terminate();

        return Status;
    }

    public void Terminate()
    {
        float resourceAmount = _owner.TryGetResourceValue("wood");
        _owner.carriedResources["wood"] = resourceAmount + 5;
        Object.Destroy(_driftwood);

        Status = GoalStatus.Completed;
    }
}