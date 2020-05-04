using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RestGoal : CompositeGoal
{
    private PlayerScript _playerScript;
    public Vector3 target;
    public RestGoal()
    {
        goalName = "Rest";
    }

    public override void Activate()
    {
        GoalStatus = GoalStatus.Active;
        target = GameObject.FindWithTag("Rest").transform.position;
        _playerScript = gameObject.GetComponent<PlayerScript>();
        _playerScript.agent.destination = target;
    }

    public override GoalStatus Process()
    {
        if (GoalStatus == GoalStatus.Inactive)
            Activate();
        if (_playerScript.agent.remainingDistance < 1)
        {
            GoalStatus = GoalStatus.Completed;
            _playerScript.energyLevel = 100;
        }

        return GoalStatus;
    }

    public override void Terminate()
    {
        Destroy(this);
    }
}
