using System;
using System.Collections.Generic;
using System.Linq;
using Entity;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

[RequireComponent(typeof(Inventory))]
public class Boat : MonoBehaviour, IAction
{
    // A dictionary with all the possible actions for the boats.
    private static Dictionary<string, Func<GoalCommand<Boat>, IGoal>> _actions;
    private BoatAutomation _goalProcessor;
    private GoalCommand<Boat> _goaldata;

    public Dictionary<string, float> carriedResources;
    public float maxCarrierAmount;
    public NavMeshAgent agent;
    public float fuel;

    public Inventory inventory;

    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _goalProcessor = new BoatAutomation(this);

        _goaldata = new GoalCommand<Boat>(this);

        this.inventory = this.gameObject.GetComponent<Inventory>();

        if (_actions == null)
        {
            // Initialize the dictionary.
            _actions = new Dictionary<string, Func<GoalCommand<Boat>, IGoal>>();
            InitGoals();
        }

        carriedResources = new Dictionary<string, float>();
        maxCarrierAmount = 20f;
    }

    // Update is called once per frame
    private void Update()
    {
        _goalProcessor.Process();
    }

    public bool ActionHandler(RaycastHit hit, bool priority)
    {
        if (!_actions.TryGetValue(hit.collider.gameObject.tag, out Func<GoalCommand<Boat>, IGoal> action)) return false;

        // Set the goal with the current data.
        this._goaldata.Position = hit.point;
        this._goaldata.Building = hit.collider.gameObject;

        IGoal goal = action.Invoke(this._goaldata);

        // Add the goal to the brain.
        if (priority)
            this._goalProcessor.AddSubGoal(goal);
        else
            this._goalProcessor.PrioritizeSubGoal(goal);

        return true;

    }

    private static void InitGoals()
    {
        Func<GoalCommand<Boat>, IGoal> goal;

        goal = input => new MoveTo(input.Owner.gameObject, input.Position, 2f);
        _actions.Add("Sea", goal);

        goal = input => new DropOff(input.Owner);
        _actions.Add("Storage", goal);

        goal = input => new Expedition(input.Owner, 100);
        _actions.Add("Expedition", goal);

        goal = input => new FetchBarrel(input.Owner, input.Building);
        _actions.Add("Barrel", goal);
    }

    public float CountResourcesCarried()
    {
        if (this.carriedResources.Count <= 0) return 0;

        return this.carriedResources.Sum(resource => resource.Value);
    }

    public float TryGetResourceValue(string resource)
    {
        if (carriedResources.TryGetValue(resource, out float amountOfResourcesCurrently))
            return amountOfResourcesCurrently;

        return 0f;
    }

    public void ClearCarriedResources() => carriedResources.Clear();
}