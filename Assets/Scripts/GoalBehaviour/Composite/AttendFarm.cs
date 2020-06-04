using Buildings.Farm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class AttendFarm : CompositeGoal
{
    private readonly Character _owner;
    private readonly Vector3 _target;
    private readonly Farm _farm;

    public AttendFarm(Character owner, Farm farm) 
    {
        Name = "AttendFarm";
        _owner = owner;
        _target = farm.transform.position;
        _farm = farm;
    }

    public override void Activate()
    {
        AddSubGoal(new MoveToAnim(_owner.gameObject, _target, 2f));

        Status = GoalStatus.Active;
    }

    public override GoalStatus Process()
    {
        if (this.SubGoals.Any(subGoal => subGoal.Status == GoalStatus.Failed))
            this.Status = GoalStatus.Failed;

        return base.Process();
    }

    public override void Terminate()
    {
        Status = GoalStatus.Completed;
    }
}
