using UnityEngine;

namespace Buildings.Farm
{
    public class SoilEmpty : IFarmingState
    {

        private Character _farmer;
        private GameObject _growthPhase;
        
        public void Start(Farm owner)
        {
            Transform trans = owner.transform;
            _growthPhase = trans.Find("GrowthPhase1").gameObject;
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
            _growthPhase.SetActive(true);
        }
    }
}