using UnityEngine;

/// <summary>
///     Test goal for trying out goal behaviour. Meeting the energy need of the agent.
/// </summary>
public class Rest : CompositeGoal
{
    private Vector3 _target;

    public Rest(Character owner, Vector3 target) : base(owner)
    {
        Name = "Rest";
    }
    
    public override void Activate()
    {
        _target = GameObject.FindGameObjectWithTag("Rest").transform.position;
        AddSubGoal(new MoveTo(Owner, _target));
        AddSubGoal(new Sleep(Owner));
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
            // Process new subgoal
            subGoals.Peek().Process();

            CheckAndRemoveCompletedSubgoals();
        }

        return Status;
    }

    public override void Terminate()
    {
        Status = GoalStatus.Completed;
    }
}