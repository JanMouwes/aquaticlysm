using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Construct : CompositeGoal
{
    private Character _owner;
    private Vector3 _target;
    private GameObject _building;

    public Construct(Character owner, GameObject building)
    {
        Name = "Build";
        _owner = owner;
        _building = building;
        _target = GetReachableLocation(_building.GetComponent<BoxCollider>(), 1.5f);
    }
    
    public override void Activate()
    {

        // Check if the target is reachable.
        if (_target == Vector3.positiveInfinity)
        {
            Status = GoalStatus.Failed;
            return;
        }

        // Add the subgoals.
        AddSubGoal(new MoveTo(_owner.gameObject, _target));
        AddSubGoal(new Build(_owner, _building));

        Status = GoalStatus.Active;
    }

    public override GoalStatus Process()
    {
        if (Status == GoalStatus.Inactive)
            Activate();

        if (Status == GoalStatus.Failed)
            return Status;

        if (SubGoals.Count == 0)
        {
            Terminate();
        }
        else
        {
            // Process new subgoal.
            SubGoals.Peek().Process();
            RemoveCompletedSubgoals();
        }

        return Status;
    }

    public override void Terminate()
    {
        Status = GoalStatus.Completed;
    }

    /// <summary>
    /// Gets a reachable point from a building close to a navgraph.
    /// </summary>
    /// <param name="box">The Boxcollider of the building.</param>
    /// <param name="maxDistance">The max distance from an object that it can be build.</param>
    /// <returns>A reachable location except if it's unreachable then it returns the positive infinite.</returns>
    private static Vector3 GetReachableLocation(BoxCollider box, float maxDistance) 
    {
        Vector3 location = box.transform.position;
        Vector3 offset = box.size / 2;

        // All accesable points of the box (north, south, east, west)
        Vector3[] points = new Vector3[]
        {
            location + new Vector3(0,0,offset.z),
            location - new Vector3(0,0,offset.z),
            location + new Vector3(offset.x,0,0),
            location - new Vector3(offset.x,0,0)
        };

        NavMeshHit hit;

        for (int i = 0; i < 4; i++)
        {
            // If the nav mesh is in reach return the location.
            if (NavMesh.SamplePosition(points[i], out hit, maxDistance, NavMesh.AllAreas))
                return hit.position;
        }  

        return Vector3.positiveInfinity;
    }
}
