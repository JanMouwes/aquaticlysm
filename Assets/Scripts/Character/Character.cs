﻿using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections.Generic;

/// <summary>
///     Basescript for agents to determine, initialize and update decisionmaking and needs.
/// </summary>
public class Character : MonoBehaviour, IAction
{
    // A dictionary with all the possible actions for the characters.
    private static Dictionary<string, Func<GoalCommand, IGoal>> _actions;

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public int Age { get; private set; }

    public NavMeshAgent agent;
    public float energyLevel;

    private Think _brain;
    private GoalCommand _goaldata;

    // Start is called before the first frame update.
    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _brain = new Think(this);
        _goaldata = new GoalCommand(this);

        if (_actions == null)
        {
            // Initialize the dictionary.
            _actions = new Dictionary<string, Func<GoalCommand, IGoal>>();
            InitGoals();
        }
    }

    // Update is called once per frame.
    private void Update()
    {
        // Check, that energylevel does not get lower during resting.
        energyLevel -= 2 * Time.deltaTime;

        _brain.Process();
    }
    
    public bool ActionHandler(RaycastHit hit, bool priority) 
    {
        if (_actions.ContainsKey(hit.collider.gameObject.tag))
        {
            // Set the goal with the current data.
            _goaldata.Position = hit.point;
            _goaldata.Building = hit.collider.gameObject;
            IGoal goal = _actions[hit.collider.gameObject.tag].Invoke(_goaldata);
        
            // Add the goal to the brain.
            if (priority)
                _brain.AddSubGoal(goal);
            else
                _brain.PrioritizeSubGoal(goal);

            return true;
        }

        return false;
    }

    /// <summary>
    /// Fills the _actions dictionary with goals related to an action.
    /// </summary>
    private static void InitGoals()
    {
        Func<GoalCommand, IGoal> goal;

        goal = input => new MoveToAnim(input.Owner.gameObject, input.Position, 2f);
        _actions.Add("Walkway", goal);
        
        goal = input => new Rest(input.Owner);
        _actions.Add("Rest", goal);
        
        goal = input =>  new Construct(input.Owner, input.Building);
        _actions.Add("Building", goal);
    }
}
