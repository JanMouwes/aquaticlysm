using System.Linq;
using Buildings.Farm;
using GoalBehaviour.Atomic;
using UnityEngine;
using UnityEngine.AI;

namespace GoalBehaviour.Composite
{
    public class StartSeeding : CompositeGoal
    {

        private Character _owner;
        private Farm _farm;
        private Vector3 _target;
        
        public StartSeeding(Character owner, Farm farm)
        {
            Name = "Start Seeding";
            _owner = owner;
            _farm = farm;
            _target = GetReachableLocation(_farm.GetComponent<BoxCollider>(), 1.5f);
        }
        
        public override void Activate()
        {
            Status = GoalStatus.Active;
            AddSubGoal(new MoveTo(_owner.gameObject, _target, 2f));
            AddSubGoal(new PlantSeeds(_owner, _farm, 2f));
        }

        public override GoalStatus Process()
        {
            if (SubGoals.Any(subGoal => subGoal.Status == GoalStatus.Failed))
                Status = GoalStatus.Failed;
            
            return base.Process();
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
            Vector3[] points = {
                location + new Vector3(0,0,offset.z),
                location - new Vector3(0,0,offset.z),
                location + new Vector3(offset.x,0,0),
                location - new Vector3(offset.x,0,0)
            };

            for (int i = 0; i < 4; i++)
            {
                // If the nav mesh is in reach return the location.
                if (NavMesh.SamplePosition(points[i], out NavMeshHit hit, maxDistance, 1 << NavMesh.GetAreaFromName("Walkable")))
                    return hit.position;
            }  

            return Vector3.positiveInfinity;
        }
    }
}