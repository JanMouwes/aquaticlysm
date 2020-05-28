using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Util;

public class Boat : MonoBehaviour, IAction
{
    // A dictionary with all the possible actions for the characters.
    private static Dictionary<string, Func<GoalCommand, IGoal>> _actions;
    private Think _brain;
    private GoalCommand _goalData;

    public NavMeshAgent agent;
    public float fuel;

    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Test for move change to actions later
        ////////////////////////////////////////////////
        #region TestCode
        if (Input.GetKeyDown(KeyCode.H))
        {
            RaycastHit hit;
            MouseUtil.TryRaycastAtMousePosition(out hit);
            agent.destination = hit.point;
        }
        #endregion
        ///////////////////////////////////////////////
    }

    private static void InitGoals()
    {
        throw new NotImplementedException();
    }

    public bool ActionHandler(RaycastHit hit, bool priority)
    {
        throw new NotImplementedException();
    }
}