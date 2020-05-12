﻿using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
///     Basescript for agents to determine, initialize and update decisionmaking and needs.
/// </summary>
public class Character : MonoBehaviour
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public int Age { get; private set; }
    public Think Brain { get; private set; }

    public float energyLevel;
    public NavMeshAgent agent;
    
    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Brain = new Think(this);
    }

    // Update is called once per frame
    private void Update()
    {
        // Check, that energylevel does not get lower during resting
        energyLevel -= 8 * Time.deltaTime;

        Brain.Process();
    }

}