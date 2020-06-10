using Actions;
using Entity;
using GoalBehaviour;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Inventory))]
public class Boat : MonoBehaviour, IClickActionComponent, IButtonActionComponent, IGoalDrivenAgent
{
    // A dictionary with all the possible actions for the boats.
    private static readonly Dictionary<string, Func<GoalCommand<Boat>, IGoal>> _actions = new Dictionary<string, Func<GoalCommand<Boat>, IGoal>>()
    {
        {"Sea", input => new MoveTo(input.Owner.gameObject, input.Position, 2f)},
        {"Storage", input => new DropOff(input.Owner)},
        {"Barrel", input => new FetchBarrel(input.Owner, input.Building)},
    };

    private BoatAutomaton _goalProcessor;
    private GoalCommand<Boat> _goaldata;
    private GameActionButtonModel[] _buttonModels;

    public Dictionary<string, float> carriedResources;
    public float maxCarrierAmount;
    public NavMeshAgent agent;
    public float fuel;

    public Inventory inventory;

    public IEnumerable<GameActionButtonModel> ButtonModels => _buttonModels;

    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _goalProcessor = new BoatAutomaton(this);
        _goaldata = new GoalCommand<Boat>(this);
        _buttonModels = GetGameActionButtonModels(this).ToArray();

        inventory = gameObject.GetComponent<Inventory>();

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
            _goaldata.Building = hit.collider.gameObject;

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
        if (carriedResources.Count <= 0) return 0;

        return carriedResources.Sum(resource => resource.Value);
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
    /// <param name="owner">The current boat.</param>
    private static IEnumerable<GameActionButtonModel> GetGameActionButtonModels(Boat owner)
    {
        yield return new GameActionButtonModel()
        {
            Name = "BoatDropOff",
            Icon = UnityEngine.Resources.Load<Sprite>("Buttons/DropOff"),
            OnClick = () => owner.PrioritiseSubgoal(new DropOff(owner))
        };
        yield return new GameActionButtonModel()
        {
            Name = "BoatExpedition",
            Icon = UnityEngine.Resources.Load<Sprite>("Buttons/Explore"),
            OnClick = () => owner.PrioritiseSubgoal(new Expedition(owner, 200))
        };        
        yield return new GameActionButtonModel()
        {
            Name = "BoatFishing",
            Icon = UnityEngine.Resources.Load<Sprite>("Buttons/Fish"),
            OnClick = () => owner.PrioritiseSubgoal(new GatherFish(owner, new Vector3(-30f, 0.57f, 20f)))
        };
    }
}