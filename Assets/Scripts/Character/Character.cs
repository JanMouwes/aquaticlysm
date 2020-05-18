using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.Experimental.TerrainAPI;
using UnityEngine.Profiling.Memory.Experimental;

/// <summary>
///     Basescript for agents to determine, initialize and update decisionmaking and needs.
/// </summary>
public class Character : MonoBehaviour, ISelectable
{
    // A dictionary with all the possible actions for the characters.
    private static Dictionary<string, Func<GoalCommand, IGoal>> _actions;

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public int Age { get; private set; }
    public bool Selected { get; set; }

    public NavMeshAgent agent;
    public float energyLevel;
    private Think _brain;
    private GoalCommand _goaldata;

    // Start is called before the first frame update.
    private void Start()
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
    
    //TODO: REMOVE HERE AND SET IN THE FACTORY.
    void OnEnable()
    {
        // Add the selectable to a list of selectable entities.
        SelectionController.selectables.Add(this);
    }

    //TODO: REMOVE HERE AND SET IN THE FACTORY.
    void OnDisable()
    {
        // Remove the selectable of the list of selectable entities.
        SelectionController.selectables.Remove(this);
    }

    // Update is called once per frame.
    private void Update()
    {
        // Check, that energylevel does not get lower during resting.
        energyLevel -= 2 * Time.deltaTime;

        _brain.Process();
    }
    
    public void ActionHandler(string tag, Vector3 position, bool priority) 
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
        }
    }

    /// <summary>
    /// Fills the _actions dictionary with goals related to an action.
    /// </summary>
    private static void InitGoals()
    {
        Func<GoalCommand, IGoal> goal;

        goal = input => new MoveTo(input.Owner, input.Position);
        _actions.Add("Walkway", goal);
        
        goal = input => new Rest(input.Owner);
        _actions.Add("Rest", goal);
    }
}
