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
    private static Dictionary<string, Func<GoalInputData, IGoal>> _actions = new Dictionary<string, Func<GoalInputData, IGoal>>();

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public int Age { get; private set; }
    public bool Selected { get; set; }

    public NavMeshAgent agent;
    public float energyLevel;

    private Think _brain;

    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _brain = new Think(this);
        InitGoals();
    }

    void OnEnable()
    {
        // Add the selectable to a list of selectable entities
        SelectionController.selectables.Add(this);
    }

    void OnDisable()
    {
        // Remove the selectable of the list of selectable entities
        SelectionController.selectables.Remove(this);
    }

    // Update is called once per frame
    private void Update()
    {
        // Check, that energylevel does not get lower during resting
        energyLevel -= 8 * Time.deltaTime;

        _brain.Process();
    }
    
    public void DoAction(string tag, Vector3 position, bool priority) 
    {
        if (_actions.ContainsKey(tag))
        {
            IGoal goal = _actions[tag].Invoke(new GoalInputData(this) { Position = position });
        
            if (priority)
                _brain.AddSubGoal(goal);
            else
                _brain.PrioritizeSubGoal(goal);
        }

    }

    private static void InitGoals()
    {
        Func<GoalInputData, IGoal> goal = input => new MoveTo(input.Owner, input.Position);
        _actions.Add("Character-Walkway", goal);
        
        goal = input => new Rest(input.Owner);
        _actions.Add("Character-Rest", goal);
        
    }
    
}