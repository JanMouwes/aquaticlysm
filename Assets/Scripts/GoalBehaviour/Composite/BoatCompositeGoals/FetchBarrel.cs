﻿using System.Linq;
using UnityEngine;

public class FetchBarrel : CompositeGoal
{
    private readonly Boat _owner;
    private readonly Vector3 _target;
    private GameObject _gameObject;

    public FetchBarrel(Boat boat, GameObject gameObject)
    {
        Name = "Fetch Barrel";
        _owner = boat;
        _gameObject = gameObject;
        _target = gameObject.transform.position;
    }

    public override void Activate()
    {
        Status = GoalStatus.Active;

        // Add subgoals
        AddSubGoal(new MoveTo(_owner.gameObject, _target, 5f));
        AddSubGoal(new OpenBarrel(_owner, _gameObject, 5f));
        AddSubGoal(new DropOff(_owner));
    }

    public override GoalStatus Process()
    {
        if (SubGoals.Any(subGoal => subGoal.Status == GoalStatus.Failed))
            Status = GoalStatus.Failed;

        return base.Process();
    }

    public override void Terminate()
    {
        Status = GoalStatus.Completed;
    }
}
