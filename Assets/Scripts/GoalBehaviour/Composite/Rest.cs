using System.Linq;
using UnityEngine;

/// <summary>
///     Test goal for trying out goal behaviour. Meeting the energy need of the agent.
/// </summary>
public class Rest : CompositeGoal
{
    private Vector3 _target;
    private Character _owner;

    public Rest(Character owner)
    {
        Name = "Rest";
        _owner = owner;
    }

    public override void Activate()
    {
        _target = GameObject.FindGameObjectWithTag("Rest").transform.position;

        // Add the subgoals.
        AddSubGoal(new MoveToAnim(_owner.gameObject, _target, 2f));
        AddSubGoal(new Sleep(_owner));

        Status = GoalStatus.Active;
    }

    public override GoalStatus Process()
    {
        if (this.SubGoals.Any(subGoal => subGoal.Status == GoalStatus.Failed)) { Terminate(); }

        return base.Process();
    }

    public override void Terminate()
    {
        Status = GoalStatus.Completed;
    }
}