using UnityEngine;

namespace Buildings.Farm
{
    public class Empty : IFarmingState
    {
        private Character _farmer;
        
        public void Start(Farm owner)
        {
        }

        public void Execute(Farm owner)
        {
            if (_farmer == null)
            {
                _farmer = owner.FindFarmer();

                if (_farmer != null)
                {
                    _farmer.StartFarming(owner);
                }
            }
            
        }

        public void Stop(Farm owner)
        {
        }
    }
}