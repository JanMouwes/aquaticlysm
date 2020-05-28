﻿using UnityEngine;

class Amass : IGoal
{
    private Boat _owner;

    public GoalStatus Status { get; private set; }

    public string Name { get; private set; }

    private float _time;
    private float _accumulateAmount;

    public Amass(Boat owner, float duration)
    {
        _owner = owner;
        _time = duration;
    }

    public void Activate()
    {
        _accumulateAmount = Random.Range(0, _owner.MaxCarrierAmount);
        _owner.GetComponent<MeshRenderer>().enabled = false;
        Status = GoalStatus.Active;
    }

    public GoalStatus Process()
    {
        if (Status == GoalStatus.Inactive)
            Activate();

        _time -= Time.deltaTime;

        if (_time <= 0)
            Terminate();

        return Status;
    }

    public void Terminate()
    {
        // SET THE STUFF HERE THAT THE GAMEOBJECT GATHERD

        _owner.GetComponent<MeshRenderer>().enabled = true;
        Status = GoalStatus.Completed;
    }
}