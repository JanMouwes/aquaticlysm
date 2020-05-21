using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildGoal : CompositeGoal
{
    private Vector3 _target;
    private GameObject _building;

    public BuildGoal(Character owner, GameObject Building) : base(owner)
    {
        Name = "Build";
        _building = Building;
        RaycastHit hit;
        Debug.Log(Building.transform.position);
    }
    
    public override void Activate()
    {
        // Add the subgoals.
        AddSubGoal(new MoveTo(Owner, _target));
        AddSubGoal(new Construct(Owner, _building));

        Status = GoalStatus.Active;
    }

    public override GoalStatus Process()
    {
        if (Status == GoalStatus.Inactive)
            Activate();

        if (subGoals.Count == 0)
        {
            Terminate();
        }
        else
        {
            // Process new subgoal.
            subGoals.Peek().Process();
            RemoveCompletedSubgoals();
        }

        return Status;
    }

    public override void Terminate()
    {
        Status = GoalStatus.Completed;
    }
}
