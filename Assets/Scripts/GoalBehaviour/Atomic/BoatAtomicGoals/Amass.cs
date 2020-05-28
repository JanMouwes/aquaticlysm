using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Amass : IGoal
{
    private Boat _owner;

    public GoalStatus Status { get; private set; }

    public string Name { get; private set; }

    private float _time;

    public Amass(Boat owner, float duration)
    {
        _owner = owner;
        _time = duration;
    }

    public void Activate()
    {
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