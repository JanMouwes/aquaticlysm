using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using Entity;
using UnityEngine.Serialization;

/// <summary>
///     Basescript for agents to determine, initialize and update decisionmaking and needs.
/// </summary>
[RequireComponent(typeof(Inventory))]
public class Character : MonoBehaviour, IAction
{
    // A dictionary with all the possible actions for the characters.
    private static Dictionary<string, Func<GoalCommand<Character>, IGoal>> _actions;

    private Think _brain;
    private GoalCommand<Character> _goaldata;

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public int Age { get; private set; }

    public Sprite Portrait { get; set; }

    public NavMeshAgent agent;
    public float energyLevel;

    public Inventory inventory;

    // Start is called before the first frame update.
    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _brain = new Think(this);
        _goaldata = new GoalCommand<Character>(this);

        this.inventory = this.gameObject.GetComponent<Inventory>();

        if (_actions == null)
        {
            // Initialize the dictionary.
            _actions = new Dictionary<string, Func<GoalCommand<Character>, IGoal>>();
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
        if (!_actions.TryGetValue(hit.collider.gameObject.tag, out Func<GoalCommand<Character>, IGoal> action)) return false;

        // Set the goal with the current data.
        this._goaldata.Position = hit.point;
        this._goaldata.Building = hit.collider.gameObject;
        IGoal goal = action.Invoke(this._goaldata);

        // Add the goal to the brain.
        if (priority)
            this._brain.AddSubGoal(goal);
        else
            this._brain.PrioritizeSubGoal(goal);

        return true;
    }

    /// <summary>
    /// Fills the _actions dictionary with goals related to an action.
    /// </summary>
    private static void InitGoals()
    {
        Func<GoalCommand<Character>, IGoal> goal;

        goal = input => new MoveToAnim(input.Owner.gameObject, input.Position, 2f);
        _actions.Add("Walkway", goal);

        goal = input => new Rest(input.Owner);
        _actions.Add("Rest", goal);

        goal = input => new Construct(input.Owner, input.Building);
        _actions.Add("Building", goal);
    }
}