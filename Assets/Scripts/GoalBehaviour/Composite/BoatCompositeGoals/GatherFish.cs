using Resources;
using System.Linq;
using UnityEngine;

public class GatherFish : CompositeGoal
{
    private readonly Boat _owner;
    private readonly Vector3 _target;

    public GatherFish(Boat boat, Vector3 target)
    { 
        Name = "Gather fish";
        _owner = boat;
        _target = target;
    }

    public override void Activate()
    {
        if (_target == Vector3.positiveInfinity)
        {
            Status = GoalStatus.Failed;
            return;
        }

        Status = GoalStatus.Active;

        // Add subgoals
        AddSubGoal(new MoveTo(_owner.gameObject, _target, 2f));
        AddSubGoal(new Fish(_owner));
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
