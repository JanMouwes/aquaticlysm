using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boat : MonoBehaviour, IAction
{
    // A dictionary with all the possible actions for the boats.
    private static Dictionary<string, Func<GoalCommand, IGoal>> _actions;
    private BoatAutomaton _goalProcessor;
    private GoalCommand _goaldata;

    public Dictionary<string, float> CarriedResources;
    public float MaxCarrierAmount;
    public NavMeshAgent agent;
    public float Fuel;

    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _goalProcessor = new BoatAutomaton(this);

        _goaldata = new GoalCommand(this);

        if (_actions == null)
        {
            // Initialize the dictionary.
            _actions = new Dictionary<string, Func<GoalCommand, IGoal>>();
            InitGoals();
        }

        CarriedResources = new Dictionary<string, float>();
        MaxCarrierAmount = 20f;
    }

    // Update is called once per frame
    private void Update()
    {
        _goalProcessor.Process();
    }
    public bool ActionHandler(RaycastHit hit, bool priority)
    {

        if (_actions.ContainsKey(hit.collider.gameObject.tag))
        {
            // Set the goal with the current data.
            _goaldata.Position = hit.point;
            IGoal goal = _actions[hit.collider.gameObject.tag].Invoke(_goaldata);

            // Add the goal to the brain.
            if (priority)
                _goalProcessor.AddSubGoal(goal);
            else
                _goalProcessor.PrioritizeSubGoal(goal);

            return true;
        }

        return false;
    }

    private static void InitGoals()
    {
        Func<GoalCommand, IGoal> goal;

        goal = input => new MoveTo(input.OwnerBoat.gameObject, input.Position);
        _actions.Add("Sea", goal);
    }

}