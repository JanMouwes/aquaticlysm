using System;
using UnityEngine;

/// <summary>
///     Test goal for trying out goal behaviour. Meeting the energy need of the agent.
/// </summary>
public class RestGoal : CompositeGoal
{
    // Position of the resting place
    public Vector3 target;
    private GameObject _owner;

    public RestGoal(GameObject owner) : base(owner)
    {
        goalName = "Rest";
        _owner = owner;
    }

    public override void Activate()
    {
        goalStatus = GoalStatus.Active;
        target = character.GetTarget(goalName);
        character.agent.destination = target;
    }

    public override GoalStatus Process()
    {
        Debug.Log(goalName);

        if (goalStatus == GoalStatus.Inactive)
            Activate();

        // Terminating condition, if energylevel 100 the goal is met.
        if (character.energyLevel >= 100.0f)
            Terminate();

        // Check, if the agent has arrived to the resting place
        if (character.agent.remainingDistance < 0.01f)
            character.energyLevel += 20 * Time.deltaTime;

        return goalStatus;
    }

    public override void Terminate()
    {
        goalStatus = GoalStatus.Completed;

        // Set other destination for the agent to see, if terminating this goal works.
        character.agent.SetDestination(new Vector3(0, 1.63f, 0));

        currentGoal = null;
    }
}