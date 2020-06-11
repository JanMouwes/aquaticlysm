using System;
using System.Linq;
using UnityEngine;

/// <summary>
///     Test goal for trying out goal behaviour. Meeting the energy need of the agent.
/// </summary>
public class Rest : CompositeGoal
{
    private GameObject _target;
    private Character _owner;

    public Rest(Character owner)
    {
        Name = "Rest";
        _owner = owner;
    }

    public override void Activate()
    {
        try
        {
            GameObject[] restPlaces = GameObject.FindGameObjectsWithTag("Rest");
            if (_target == null)
            {
                _target = restPlaces[0];
            }
            foreach (GameObject restPlace in restPlaces)
            {
                float currentDist = Vector3.Distance(_owner.transform.position, restPlace.transform.position);
                if (currentDist < Vector3.Distance(_owner.transform.position, _target.transform.position))
                    _target = restPlace;
            }
            // Add the subgoals.
            AddSubGoal(new MoveToAnim(_owner.gameObject, _target.transform.position, 2f));
            AddSubGoal(new Sleep(_owner, _target.transform.position));
            Status = GoalStatus.Active;
        }
        catch(Exception e)
        {
            Console.WriteLine("{0} Exception caught.", e);
            Status = GoalStatus.Failed;
        }
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