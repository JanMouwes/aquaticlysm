using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Util;

public class Boat : MonoBehaviour, IAction
{
    // A dictionary with all the possible actions for the characters.
    private static Dictionary<string, Func<GoalCommand, IGoal>> _actions;
    private Think _brain;
    private GoalCommand _goaldata;

    public NavMeshAgent agent;
    public float fuel;

    public bool ActionHandler(string tag, Vector3 position, bool priority)
    {
        if (_actions.ContainsKey(tag))
        {
            // Set the goal with the current data.
            _goaldata.Position = position;
            IGoal goal = _actions[tag].Invoke(_goaldata);

            // Add the goal to the brain.
            if (priority)
                _brain.AddSubGoal(goal);
            else
                _brain.PrioritizeSubGoal(goal);

            return true;
        }
        return false;
    }

    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (_actions == null)
        {
            // Initialize the dictionary.
            _actions = new Dictionary<string, Func<GoalCommand, IGoal>>();
            InitGoals();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // Test for move change to actions later
        ////////////////////////////////////////////////
        #region TestCode
        if (Input.GetKeyDown(KeyCode.H))
        {
            RaycastHit hit;
            MouseUtil.TryRaycastAtMousePosition(out hit);
            agent.destination = hit.point;
        }
        #endregion
        ///////////////////////////////////////////////
    }

    private static void InitGoals()
    {
        Func<GoalCommand, IGoal> goal;

        goal = input => new MoveTo(input.Owner, input.Position);
        _actions.Add("Sea", goal);
    }
}