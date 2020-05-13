using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
///     Basescript for agents to determine, initialize and update decisionmaking and needs.
/// </summary>
public class Character : MonoBehaviour, ISelectable
{
    private static Dictionary<string, IGoal> _actions = new Dictionary<string, IGoal>();

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
        IGoal goal = null;

        switch (tag)
        {
            case "Walkway":
                    goal = new MoveTo(this, position);
                break;
            case "Rest":
                    goal = new Rest(this, position);
                break;
            case "Boat":
                Debug.Log("Booty");
                break;
            default:
                Debug.Log("I can't do that");
                break;
        }

        if (goal != null && priority)
            _brain.AddSubGoal(goal);
        else
            _brain.PrioritizeSubGoal(goal);
    }
}