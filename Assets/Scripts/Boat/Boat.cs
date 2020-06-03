using System;
using System.Collections.Generic;
using System.Linq;
using Actions;
using Actions.GameActions;
using GoalBehaviour;
using UnityEngine;
using UnityEngine.AI;

public class Boat : MonoBehaviour, IActionComponent, IGoalDrivenAgent
{
    // A dictionary with all the possible actions for the boats.
    // private static Dictionary<string, Func<GoalCommand<Boat>, IGoal>> _actions;
    private BoatAutomaton _goalProcessor;
    private GoalCommand<Boat> _goaldata;

    private GameAction[] _actions;
    public IEnumerable<GameAction> Actions => this._actions;

    private GameActionButtonModel[] _buttonModels;
    public IEnumerable<GameActionButtonModel> ButtonModels => this._buttonModels;

    public Dictionary<string, float> carriedResources;
    public float maxCarrierAmount;
    public NavMeshAgent agent;
    public float fuel;

    private Action<GoalCommand<Boat>> _handler;

    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _goalProcessor = new BoatAutomaton(this);

        _goaldata = new GoalCommand<Boat>(this);

        this._buttonModels = GetGameActionButtonModels(this).ToArray();

        if (_actions == null)
        {
            // this._actions = GetGoalTypes()
            //                .Select(tuple => new SetGoalGameAction<Boat>(tuple.goal, "action-boat-" + tuple.hitTagName, this))
            //                .Cast<GameAction>()
            //                .ToArray();
        }

        carriedResources = new Dictionary<string, float>();
        maxCarrierAmount = 20f;
    }

    // Update is called once per frame
    private void Update()
    {
        _goalProcessor.Process();
    }

    public bool HandleAction(RaycastHit hit, bool priority)
    {
        if (this._handler != null)
        {
            GoalCommand<Boat> command = new GoalCommand<Boat>(this)
            {
                Position = hit.point
            };

            this._handler.Invoke(command);

            return true;
        }
        // else if (_actions.ContainsKey(hit.collider.gameObject.tag))
        // {
        //     // Set the goal with the current data.
        //     _goaldata.Position = hit.point;
        //     IGoal goal = _actions[hit.collider.gameObject.tag].Invoke(_goaldata);
        //
        //     // Add the goal to the brain.
        //     if (priority)
        //         _goalProcessor.AddSubGoal(goal);
        //     else
        //         _goalProcessor.PrioritizeSubGoal(goal);
        //
        //     return true;
        // }

        return false;
    }

    private static IEnumerable<(string hitTagName, Func<GoalCommand<Boat>, IGoal> goal)> GetGoalTypes()
    {
        yield return ("Sea", input => new MoveTo(input.Owner.gameObject, input.Position, 2f));

        yield return ("Storage", input => new DropOff(input.Owner));

        yield return ("Expedition", input => new Expedition(input.Owner, 100));
    }

    private static IEnumerable<GameActionButtonModel> GetGameActionButtonModels(Boat owner)
    {
        yield return new GameActionButtonModel()
        {
            Name = "BoatMove",
            Icon = UnityEngine.Resources.Load<Sprite>("Sprites/John"),
            OnClick = () =>
            {
                Debug.Log("clicked!");
                owner._handler = input => owner.AddSubgoal(new MoveTo(input.Owner.gameObject, input.Position, 2f));
            }
        };
        yield return new GameActionButtonModel()
        {
            Name = "BoatDropOff",
            Icon = UnityEngine.Resources.Load<Sprite>("Sprites/George")
        };
        yield return new GameActionButtonModel()
        {
            Name = "BoatExpedition",
            Icon = UnityEngine.Resources.Load<Sprite>("Sprites/Janica")
        };

        // yield return ("Storage", input => new DropOff(input.Owner));
        //
        // yield return ("Expedition", input => new Expedition(input.Owner, 100));
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

    public void AddSubgoal(IGoal goal) => this._goalProcessor.AddSubGoal(goal);

    public void PrioritiseSubgoal(IGoal goal) => this._goalProcessor.PrioritizeSubGoal(goal);
}