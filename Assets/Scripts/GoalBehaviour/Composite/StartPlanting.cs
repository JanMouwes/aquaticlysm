using System.Linq;
using Buildings.Farm;
using GoalBehaviour.Atomic;
using UnityEngine;

namespace GoalBehaviour.Composite
{
    public class StartPlanting : CompositeGoal
    {

        private Character _owner;
        private Farm _farm;
        private Vector3 _target;
        
        public StartPlanting(Character owner, Farm farm)
        {
            Name = "Start Planting";
            _owner = owner;
            _farm = farm;
            _target = farm.transform.position;
        }
        
        public override void Activate()
        {
            Status = GoalStatus.Active;

            AddSubGoal(new MoveTo(_owner.gameObject, _target, 2f));
            AddSubGoal(new PlantSeeds(_owner, _farm, _target, 20f));
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


    }
}