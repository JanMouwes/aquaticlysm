using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Amass : IGoal
{
    private GameObject _owner;

    public GoalStatus Status { get; private set; }

    public string Name { get; private set; }

    public Amass(GameObject owner, float duration)
    {
        _owner = owner;

    }

    public void Activate()
    {
        throw new NotImplementedException();
    }

    public GoalStatus Process()
    {
        throw new NotImplementedException();
    }

    public void Terminate()
    {
        throw new NotImplementedException();
    }
}