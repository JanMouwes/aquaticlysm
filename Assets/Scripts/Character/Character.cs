﻿using Actions;
using Entity;
using GoalBehaviour;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
///     Basescript for agents to determine, initialize and update decisionmaking and needs.
/// </summary>
[RequireComponent(typeof(Inventory))]
public class Character : MonoBehaviour, IClickActionComponent, IButtonActionComponent, IGoalDrivenAgent
{
    // A dictionary with all the possible actions for the characters.
    private static Dictionary<string, Func<GoalCommand<Character>, IGoal>> _actions = new Dictionary<string, Func<GoalCommand<Character>, IGoal>>()
    {
        { "Walkway", input => new MoveToAnim(input.Owner.gameObject, input.Position, 2f) },
        { "House", input => new Rest(input.Owner) },
        { "Building", input => new Construct(input.Owner, input.Building) },
        { "Character", input => new MoveToAnim(input.Owner.gameObject, input.Position, 2f) },
    };

    public IEnumerable<GameActionButtonModel> ButtonModels => _buttonModels;

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public int Age { get; private set; }

    public Sprite Portrait { get; set; }

    public NavMeshAgent agent;
    public float energyLevel;

    private Think _brain;
    private GoalCommand<Character> _goaldata;
    private GameActionButtonModel[] _buttonModels;
    public Inventory inventory;

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        inventory = gameObject.GetComponent<Inventory>();

        _brain = new Think(this);
        _goaldata = new GoalCommand<Character>(this);
        _buttonModels = GetGameActionButtonModels(this).ToArray();
    }

    private void Update()
    {
        // Check, that energylevel does not get lower during resting.
        energyLevel -= 0.5f * Time.deltaTime;

        _brain.Process();
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

    public void AddSubgoal(IGoal goal) => _brain.AddSubGoal(goal);

    public void PrioritiseSubgoal(IGoal goal) => _brain.PrioritizeSubGoal(goal);

    /// <summary>
    /// All button actions.
    /// </summary>
    /// <param name="owner">The current character.</param>
    private static IEnumerable<GameActionButtonModel> GetGameActionButtonModels(Character owner)
    {
        yield return new GameActionButtonModel()
        {
            Name = "CharacterRest",
            Icon = UnityEngine.Resources.Load<Sprite>("Buttons/Sleep"),
            OnClick = () => owner.PrioritiseSubgoal(new Rest(owner))
        };
    }
}