using Resources;
using System.Linq;
using UnityEngine;

public class GatherFish : CompositeGoal
{
    private readonly Boat _owner;
    private readonly Vector3 _target;
    private readonly Vector3 _returnTarget;

    public GatherFish(Boat boat, Vector3 target)
    { 
        Name = "Gather fish";
        _owner = boat;
        _target = target;
        _returnTarget = GameObject.FindGameObjectWithTag("Storage").GetComponent<GameObject>().transform.position;
    }

    public override void Activate()
    {
        if (_target == Vector3.positiveInfinity)
        {
            Status = GoalStatus.Failed;
            return;
        }

        // Add subgoals
        AddSubGoal(new MoveTo(_owner.gameObject, _target));
        AddSubGoal(new Fish(_owner));
        AddSubGoal(new MoveTo(_owner.gameObject, _returnTarget));
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
