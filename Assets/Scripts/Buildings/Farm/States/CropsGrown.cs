using Resources;
using UnityEngine;

namespace Buildings.Farm
{
    public class CropsGrown : IFarmingState
    {
        private Character _farmer;
        private GameObject _oldGrowthPhase;
        private GameObject _growthPhase;

        public void Start(Farm owner)
        {
            Transform trans = owner.transform;

            _oldGrowthPhase = trans.Find("GrowthPhase5").gameObject;
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
            _oldGrowthPhase.SetActive(false);
        }
    }
}