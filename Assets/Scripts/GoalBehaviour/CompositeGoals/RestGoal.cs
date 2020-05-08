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
        target = GameObject.FindGameObjectWithTag("Rest").transform.position;
        character.agent.destination = target;
    }

    public override GoalStatus Process()
    {
        Debug.Log(goalName);

        if (goalStatus == GoalStatus.Inactive)
            Activate();

        // Terminating condition, if energylevel 100 the goal is met.
        if (isRested(character.energyLevel))
            Terminate();

        // Check, if the agent has arrived to the resting place
        if (character.agent.remainingDistance < 0.01f)
            character.energyLevel += 20 * Time.deltaTime;

        return goalStatus;
    }

    public override void Terminate()
    {
        goalStatus = GoalStatus.Completed;
    }

    /// <summary>
    /// Check, if energylevel has been met or not.
    /// </summary>
    /// <param name="energyLevel"></param>
    /// <returns></returns>
    private static bool isRested(float energyLevel) => energyLevel >= 100.0f;
}