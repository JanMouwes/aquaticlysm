using UnityEngine;

class BoatAutomaton : CompositeGoal
    {
        private Boat _owner;

        public BoatAutomaton(Boat owner)
        {
            Name = "Process boat commands";
            this._owner = owner;
        }

        /// <summary>
        ///     Checks if there is a need for a new goal.
        /// </summary>
        public void FindSubGoal()
        {
           // if (needsToGatherFish(_owner.)) { AddSubGoal(new GatherFish(_owner, new Vector3(-30f, 0.57f, 20f)));  }
        }

        public override void Activate()
        {
            Status = GoalStatus.Active;
        }

        public override GoalStatus Process()
        {
            if (Status == GoalStatus.Inactive)
                Activate();

            if (SubGoals.Count == 0) { FindSubGoal(); }

            return base.Process();
        }

        public override void Terminate() { }

    public static bool needsToGatherFish(float fishAmount) => fishAmount <= 20f;
    }
