using Actions;
using GoalBehaviour;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Boat : MonoBehaviour, IActionComponent, IGoalDrivenAgent
{
    // A dictionary with all the possible actions for the boats.
    private static readonly Dictionary<string, Func<GoalCommand<Boat>, IGoal>> _actions = new Dictionary<string, Func<GoalCommand<Boat>, IGoal>>()
    {
        { "Sea", input => new MoveTo(input.Owner.gameObject, input.Position, 2f) },
        { "Storage", input => new DropOff(input.Owner) },
    };

    public IEnumerable<GameActionButtonModel> ButtonModels => _buttonModels;

    public Dictionary<string, float> carriedResources;
    public float maxCarrierAmount;
    public NavMeshAgent agent;
    public float fuel;

    private BoatAutomaton _goalProcessor;
    private GoalCommand<Boat> _goaldata;
    private GameActionButtonModel[] _buttonModels;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _goalProcessor = new BoatAutomaton(this);
        _goaldata = new GoalCommand<Boat>(this);

        _buttonModels = GetGameActionButtonModels(this).ToArray();

        carriedResources = new Dictionary<string, float>();
        maxCarrierAmount = 20f;
    }

    private void Update()
    {
        _goalProcessor.Process();
    }

    public bool HandleAction(RaycastHit hit, bool priority)
    {
        if (_actions.ContainsKey(hit.collider.gameObject.tag))
        {
            // Set the goal with the current data.
            _goaldata.Position = hit.point;
            IGoal goal = _actions[hit.collider.gameObject.tag].Invoke(_goaldata);

            // Add the goal to the brain.
            if (priority)
                AddSubgoal(goal);
            else
                PrioritiseSubgoal(goal);

            return true;
        }

        return false;
    }

    public float CountResourcesCarried()
    {
        float resources = 0f;

        if (carriedResources.Count > 0)
        {
            foreach (KeyValuePair<string, float> resource in carriedResources) { resources += resource.Value; }
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

    public void AddSubgoal(IGoal goal) => _goalProcessor.AddSubGoal(goal);

    public void PrioritiseSubgoal(IGoal goal) => _goalProcessor.PrioritizeSubGoal(goal);

    /// <summary>
    /// All button actions.
    /// </summary>
    /// <param name="owner">The boat.</param>
    private static IEnumerable<GameActionButtonModel> GetGameActionButtonModels(Boat owner)
    {
        yield return new GameActionButtonModel()
        {
            Name = "BoatDropOff",
            Icon = UnityEngine.Resources.Load<Sprite>("Icons/Boxes"),
            OnClick = () => owner.PrioritiseSubgoal(new DropOff(owner))
        };
        yield return new GameActionButtonModel()
        {
            Name = "BoatExpedition",
            Icon = UnityEngine.Resources.Load<Sprite>("Icons/Compass"),
            OnClick = () => owner.PrioritiseSubgoal(new Expedition(owner, 100))
        };
    }
}