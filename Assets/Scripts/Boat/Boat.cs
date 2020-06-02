using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Boat : MonoBehaviour, IAction
{
    // A dictionary with all the possible actions for the boats.
    private static Dictionary<string, Func<GoalCommand, IGoal>> _actions;
    private BoatAutomaton _goalProcessor;
    private GoalCommand _goaldata;

    public Dictionary<string, float> carriedResources;
    public float maxCarrierAmount;
    public NavMeshAgent agent;
    public float fuel;

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

        if (_actions.ContainsKey(hit.collider.gameObject.tag))
        {
            // Set the goal with the current data.
            _goaldata.Position = hit.point;
            _goaldata.Building = hit.collider.gameObject;

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

        goal = input => new MoveTo(input.OwnerBoat.gameObject, input.Position, 2f);
        _actions.Add("Sea", goal);

        goal = input => new DropOff(input.OwnerBoat);
        _actions.Add("Storage", goal);

        goal = input => new Expedition(input.OwnerBoat, 100);
        _actions.Add("Expedition", goal);
       
        goal = input => new FetchBarrel(input.OwnerBoat, input.Building);
        _actions.Add("Barrel", goal);
    }

    public float CountResourcesCarried()
    {
        float resources = 0f;

        if (carriedResources.Count > 0)
        {
            foreach (KeyValuePair<string, float> resource in carriedResources)
            {
                resources += resource.Value;
            }
        }

        return resources;
    }

    public float TryGetResourceValue(string resource)
    {
        if (carriedResources.TryGetValue(resource, out float amountOfResourcesCurrently))
            return amountOfResourcesCurrently;

        return 0f;
    }

    public void ClearCarriedResources() => carriedResources.Clear();
}