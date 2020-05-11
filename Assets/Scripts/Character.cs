using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public enum Profession { None, Doctor, Farmer, Fisher }
public enum Skill { None, Medical, Farming, Fishing}

/// <summary>
///     Basescript for agents to determine, initialize and update decisionmaking and needs.
/// </summary>
public class Character : MonoBehaviour
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public int Age { get; private set; }

    public float energyLevel;
    public Profession profession;
    public ushort[] skills;
    public NavMeshAgent agent;
    private ThinkGoal _brain;


    // Start is called before the first frame update
    private void Start()
    {
        energyLevel = 100;
        profession = Profession.None;
        skills = new ushort[System.Enum.GetValues(typeof(Skill)).Length];
        
        agent = GetComponent<NavMeshAgent>();
        _brain = new ThinkGoal(this);
    }

    // Update is called once per frame
    private void Update()
    {
        // Check, that energylevel does not get lower during resting
        energyLevel -= 8 * Time.deltaTime;

        _brain.Process();
    }
}