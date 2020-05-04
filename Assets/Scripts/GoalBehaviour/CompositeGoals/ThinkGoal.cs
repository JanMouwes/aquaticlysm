using System.Collections;
using System.Collections.Generic;
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
        GoalStatus = GoalStatus.Active;
        _playerScript = GetComponent<PlayerScript>();
    }

    public override GoalStatus Process()
    {
        if (_playerScript.energyLevel < 10 && gameObject.GetComponent<RestGoal>() == null)
        {
            RestGoal restGoal = gameObject.AddComponent<RestGoal>();
            AddSubGoal(restGoal);
        }

        
        // Activate, if goalstatus not yet active
        if (GoalStatus == GoalStatus.Inactive)
            Activate();
        
        
        // Make sure all completed and failed subgoals are removed from the subgoal list
        foreach (var subGoal in subGoals)
        {
            if (subGoal.GoalStatus == GoalStatus.Completed || subGoal.GoalStatus == GoalStatus.Failed)
            {
                subGoal.Terminate();
            }
        }
        subGoals.RemoveAll(sg => sg.GoalStatus == GoalStatus.Completed || sg.GoalStatus == GoalStatus.Failed);

        // Process new subgoals
        subGoals.ForEach(sg => sg.Process());

        
        
        return GoalStatus;
    }

    public override void Terminate()
    {
        throw new System.NotImplementedException();
    }
}
