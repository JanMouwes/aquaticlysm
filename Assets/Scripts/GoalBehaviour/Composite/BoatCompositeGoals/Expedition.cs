using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Expedition : CompositeGoal
{
    private readonly Boat _owner;
    private readonly int _gridSize;
    public Expedition(Boat owner, int gridSize)
    {
        _owner = owner;
        _gridSize = gridSize;
    }

    public override void Activate()
    {
        AddSubGoal(new MoveTo(_owner.gameObject, GetRandomOutermostEdgeVector3(_gridSize)));
        AddSubGoal(new Amass(_owner, Random.Range(20f, 50f)));
        //AddSubGoal(new MoveTo(_owner.gameObject, _returnTarget));

        Status = GoalStatus.Active;
    }

    public override GoalStatus Process()
    {
        if (this.SubGoals.Any(subGoal => subGoal.Status == GoalStatus.Failed))
            Status = GoalStatus.Failed;

        return base.Process();
    }

    public override void Terminate()
    {
        Status = GoalStatus.Completed;
    }

    public static Vector3 GetRandomOutermostEdgeVector3(int size) 
    {
        if (0.5 < Random.value)
            return new Vector3
            {
                x = Random.Range(-size, size),
                y = 0,
                z = (0.5 < Random.value) ? -size : size
            };
        else
            return new Vector3
            {
                x = (0.5 < Random.value) ? -size : size,
                y = 0,
                z = Random.Range(-size, size)
            };
    }
}
