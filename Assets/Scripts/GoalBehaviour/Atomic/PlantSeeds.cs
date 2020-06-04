using Buildings.Farm;
using Resources;
using UnityEngine;

namespace GoalBehaviour.Atomic
{
    public class PlantSeeds : IGoal
    {
        public GoalStatus Status { get; private set; }
        public string Name { get; }

        private Character _owner;
        private readonly Farm _farm;
        private readonly Vector3 _target;
        private float _time;
        
        public PlantSeeds(Character owner, Farm farm, float duration)
        {
            Name = "PlantSeeds";
            _owner = owner;
            _farm = farm;
            _target = farm.transform.position;
            _time = duration;
        }
        
        public void Activate()
        {
            Status = GoalStatus.Active;
        }

        public GoalStatus Process()
        {
            if (Status == GoalStatus.Inactive)
                Activate();
            
            _time -= Time.deltaTime;
            
            if (_time <= 0)
                Plant();

            return Status;
        }

        public void Terminate()
        {
            Status = GoalStatus.Completed;
        }

        private void Plant()
        {
            // Change status of Farm to first growing phase.
            if (ResourceManager.Instance.DecreaseResource("water", 15))
            {
                _farm.ChangeState(new Growing());
                Terminate();
            }
            else
            {
                Debug.Log("Not enough water to plant seed!");
                Status = GoalStatus.Failed;
            }
        }
    }
}