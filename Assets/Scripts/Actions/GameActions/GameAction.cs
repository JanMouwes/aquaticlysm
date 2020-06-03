using UnityEngine;

namespace Actions.GameActions
{
    public abstract class GameAction
    {
        public string Name { get; }

        /// <summary>
        /// The action handler processes all the selectable specific actions.
        /// </summary>
        /// <param name="hit">Where the player clicked</param>
        /// <param name="priority">Is it a prioritized action or not</param>
        public abstract void HandleAction(RaycastHit hit, bool priority);
        
        protected GameAction(string name)
        {
            this.Name = name;
        }
    }
}