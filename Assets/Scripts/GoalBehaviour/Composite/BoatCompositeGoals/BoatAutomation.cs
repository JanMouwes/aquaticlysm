using System.Linq;
using UnityEngine;

class BoatAutomation : CompositeGoal
    {
        private Boat _owner;

        public BoatAutomation(Boat owner)
        {
            Name = "Process boat commands";
            this._owner = owner;
        }

        public override void Activate()
        {
            Status = GoalStatus.Active;
        }

        public override GoalStatus Process()
        {
            if (Status == GoalStatus.Inactive)
                Activate();

            return base.Process();
        }

        public override void Terminate() { }
    }
