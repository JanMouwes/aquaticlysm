using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : IGoal
{
    public Character Owner { get; private set; }
    public GoalStatus Status { get; private set; }
    public string Name { get; private set; }

    // Target position
    private Vector3 _target;

    public MoveTo(Character owner, Vector3 position)
    {
        Owner = owner;
        Name = "MoveTo";
        _target = position;
    }

    public void Activate()
    {
        Owner.agent.destination = _target;
        Status = GoalStatus.Active;
    }

    public GoalStatus Process()
    {
        Debug.Log(Name);

        if (Status == GoalStatus.Inactive)
            Activate();

        if (Vector3.Distance(Owner.transform.position, _target) < 2f)
            Terminate();

        return Status;
    }

    public void Terminate()
    {
        Status = GoalStatus.Completed;
    }
}
