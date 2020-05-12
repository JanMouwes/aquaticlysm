using UnityEngine;

/// <summary>
///     Test goal for trying out goal behaviour. Meeting the energy need of the agent.
/// </summary>
public class Rest : CompositeGoal
{
    public Character Owner { get; set; }
    public GoalStatus Status { get; set; }
    public string Name { get; set; }

    private Vector3 _target;

    public Rest(Character owner, Vector3 target) : base(owner)
    {
        Owner = owner;
        Status = GoalStatus.Inactive;
        Name = "Rest";
    }
    
    public override void Activate()
    {
        Status = GoalStatus.Active;
        _target = GameObject.FindGameObjectWithTag("Rest").transform.position;
        AddSubGoal(new MoveTo(Owner, _target));
        AddSubGoal(new Sleep(Owner));
    }

    public override GoalStatus Process()
    {
        Debug.Log(Name);
        Debug.Log(subGoals.Count);

        if (Status == GoalStatus.Inactive)
            Activate();

        if (subGoals.Count == 0)
            Terminate();
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

    /// <summary>
    ///     Check, if energylevel has been met or not.
    /// </summary>
    /// <param name="energyLevel"></param>
    /// <returns></returns>
    private static bool isRested(float energyLevel)
    {
        return energyLevel >= 100.0f;
    }
}