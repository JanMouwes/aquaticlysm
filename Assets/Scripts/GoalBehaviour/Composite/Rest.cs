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
        AddSubGoal(new MoveTo(_owner.gameObject, _target));
        AddSubGoal(new Sleep(_owner));

        Status = GoalStatus.Active;
    }

    public override GoalStatus Process()
    {
        if (Status == GoalStatus.Inactive)
            Activate();

        if (SubGoals.Count == 0)
        {
            Terminate();
        }
        else
        {
            // Process new subgoal.
            SubGoals.Peek().Process();
            RemoveCompletedSubgoals();
        }

        return Status;
    }

    public override void Terminate()
    {
        Status = GoalStatus.Completed;
    }
}
