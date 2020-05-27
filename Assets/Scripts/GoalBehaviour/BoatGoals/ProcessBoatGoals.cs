    class ProcessBoatGoals : CompositeGoal
    {
        private Boat _owner;

        public ProcessBoatGoals(Boat owner)
        {
            Name = "Process boat commands";
            this._owner = owner;
        }

        /// <summary>
        ///     Checks if there is a need for a new goal.
        /// </summary>
        public void FindSubGoal()
        {
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
    }
