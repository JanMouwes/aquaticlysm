using System;
using System.IO;
using Buildings.Farm;
using Resources;
using UnityEngine;
using UnityEngine.AI;

namespace GoalBehaviour.Atomic
{
    public class HarvestCrop : IGoal
    {
        public GoalStatus Status { get; private set; }
        public string Name { get; private set; }
        
        private Character _owner;
        private Farm _farm;
        private Vector3 _target;
        private float _time;

        public HarvestCrop(Character owner, Farm farm, float duration)
        {
            Name = "Harvest crops";
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
                Harvest();

            return Status;
        }

        public void Terminate()
        {
            Status = GoalStatus.Completed;
        }

        private void Harvest()
        {
            // Change status of Farm to first growing phase.
            ResourceManager.Instance.IncreaseResource("food", 50);
            _farm.ChangeState(new SoilEmpty());
            Terminate();
            
        }
    }
}