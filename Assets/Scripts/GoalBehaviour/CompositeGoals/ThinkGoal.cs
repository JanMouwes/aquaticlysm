﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Highest level of goal managing, makes decisions between strategies to
/// fulfill agents pressing needs.
/// </summary>
public class ThinkGoal : CompositeGoal
{
    private PlayerScript _playerScript;

    public ThinkGoal()
    {
        goalName = "Think";
        // To be continued...
    }

    public override void Activate()
    {
        goalStatus = GoalStatus.Active;
        _playerScript = GetComponent<PlayerScript>();
    }

    public override GoalStatus Process()
    {
        Debug.Log(goalName);
        // Activate, if goalstatus not yet active
        if (goalStatus == GoalStatus.Inactive)
            Activate();

        // Check if there is need for resting and also that the agent is not currently taking care of it
        if (_playerScript.energyLevel < 10 && gameObject.GetComponent<RestGoal>() == null)
        {
            RestGoal restGoal = gameObject.AddComponent<RestGoal>();
            AddSubGoal(restGoal);
        }
        
        // Make sure all completed and failed subgoals are removed from the subgoal list
        subGoals.RemoveAll(sg => sg.goalStatus == GoalStatus.Completed || sg.goalStatus == GoalStatus.Failed);

        // Process new subgoals
        subGoals.ForEach(sg => sg.Process());
        
        return goalStatus;
    }
    
    public override void Terminate()
    {
        throw new System.NotImplementedException();
    }
}
