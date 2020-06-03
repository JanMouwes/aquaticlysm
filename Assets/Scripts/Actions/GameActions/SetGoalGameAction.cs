using System;
using GoalBehaviour;
using UnityEngine;

namespace Actions.GameActions
{
    public class SetGoalGameAction<TOwner> : GameAction where TOwner : MonoBehaviour, IGoalDrivenAgent
    {
        private readonly TOwner _owner;

        private Func<GoalCommand<TOwner>, IGoal> _handler;

        public SetGoalGameAction(Func<GoalCommand<TOwner>, IGoal> handler, string name, TOwner owner) : base(name)
        {
            this._handler = handler;
            this._owner = owner;
        }

        public override void HandleAction(RaycastHit hit, bool priority)
        {
            GoalCommand<TOwner> goalCommand = new GoalCommand<TOwner>(this._owner)
            {
                Position = hit.point
            };

            IGoal goal = this._handler?.Invoke(goalCommand);

            if (priority) { this._owner.AddSubgoal(goal); }
            else { this._owner.PrioritiseSubgoal(goal); }
        }
    }
}