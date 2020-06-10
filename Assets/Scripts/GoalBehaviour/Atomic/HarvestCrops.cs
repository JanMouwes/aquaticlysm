using Buildings.Farm;
using Buildings.Farm.States;
using Resources;
using UnityEngine;

namespace GoalBehaviour.Atomic
{
    public class HarvestCrops : IGoal
    {
        private Character _owner;
        private Farm _farm;
        private float _time;

        public GoalStatus Status { get; private set; }
        public string Name { get; }

        public HarvestCrops(Character owner, Farm farm, float duration)
        {
            Name = "Harvest crops";
            _owner = owner;
            _farm = farm;
            _time = duration;
        }

        public void Activate()
        {
            if (!this._farm.TryClaimFarm(this._owner))
            {
                Status = GoalStatus.Failed;

                return;
            }

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
            this._farm.ReleaseFarm(this._owner);
        }

        private void Harvest()
        {
            ResourceManager resourceManager = Object.FindObjectOfType<ResourceManager>();
            // Change status of Farm to first growing phase.
            resourceManager.IncreaseResource("food", this._farm.harvestAmount);
            _farm.ChangeState(Empty.Instance);

            Terminate();
        }
    }
}