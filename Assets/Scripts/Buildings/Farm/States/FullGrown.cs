using Resources;
using UnityEngine;

namespace Buildings.Farm
{
    public class FullGrown : IFarmingState
    {
        private Character _farmer;
        private GameObject _growthPhase;

        public void Start(Farm owner)
        {
            _growthPhase = owner.transform.Find("GrowthPhase5").gameObject;
        }

        public void Execute(Farm owner)
        {
            if (_farmer == null)
            {
                _farmer = owner.FindFarmer();

                if (_farmer != null)
                {
                    _farmer.StartHarvesting(owner);
                }
            }
        }

        public void Stop(Farm owner)
        {
            _growthPhase.SetActive(false);
        }
    }
}