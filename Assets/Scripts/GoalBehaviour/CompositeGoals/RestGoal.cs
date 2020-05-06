using System;
using UnityEngine;

/// <summary>
///     Test goal for trying out goal behaviour. Meeting the energy need of the agent.
/// </summary>
public class RestGoal : CompositeGoal
{
    private PlayerScript _playerScript;

    // Position of the resting place
    public Vector3 target;

    public RestGoal()
    {
        goalName = "Rest";
    }

    public override void Activate()
    {
        goalStatus                      = GoalStatus.Active;
        target                          = GameObject.FindWithTag("Rest").transform.position;
        _playerScript                   = gameObject.GetComponent<PlayerScript>();
        _playerScript.agent.destination = target;
    }

    public override GoalStatus Process()
    {
        Debug.Log(goalName);
        if (goalStatus == GoalStatus.Inactive)
            Activate();

        // Terminating condition, if energylevel 100 the goal is met.
        if (_playerScript.energyLevel >= 100.0f)
            Terminate();

        // Check, if the agent has arrived to the resting place
        if (_playerScript.agent.remainingDistance < 0.01f)
            _playerScript.energyLevel += 10 * Time.deltaTime;

        return goalStatus;
    }

    public override void Terminate()
    {
        goalStatus = GoalStatus.Completed;

        // Set other destination for the agent to see, if terminating this goal works.
        _playerScript.agent.SetDestination(new Vector3(0, 1.63f, 0));

        Destroy(this);
    }
}