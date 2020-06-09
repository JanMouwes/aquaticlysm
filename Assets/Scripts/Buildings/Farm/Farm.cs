using System.Collections.Generic;
using System.Linq;
using Buildings.Farm.States;
using UnityEngine;

namespace Buildings.Farm
{
    public class Farm : MonoBehaviour
    {
        private static LinkedList<Farm> _farms = new LinkedList<Farm>();
        public static IEnumerable<Farm> Farms => _farms;
        public static IEnumerable<Farm> FarmsWithState<TState>() where TState : FarmingState => Farms.Where(farm => farm._state is TState);

        private FarmingState _state;

        public Character CurrentFarmer { get; private set; }

        public int harvestAmount;

        private void Start()
        {
            _state = new Disabled();
            _state.Start(this);

            _farms.AddLast(this);
        }

        /// <summary>
        /// Claims farm if not already claimed
        /// </summary>
        /// <param name="farmer">Farmer to claim farm</param>
        /// <returns>Whether it was successful</returns>
        public bool TryClaimFarm(Character farmer)
        {
            if (this.CurrentFarmer != null && CurrentFarmer != farmer) { return false; }

            this.CurrentFarmer = farmer;

            return true;
        }

        public bool IsClaimed => CurrentFarmer != null;

        /// <summary>
        /// Releases farm if farmer is currentFarmer
        /// </summary>
        /// <param name="farmer">Farmer to release farm</param>
        public void ReleaseFarm(Character farmer)
        {
            if (CurrentFarmer == farmer) { CurrentFarmer = null; }
        }

        public void ChangeState(FarmingState state)
        {
            _state.Stop(this);
            _state = state;
            _state.Start(this);
        }

        private void Update()
        {
            _state.Execute(this);
        }
    }
}