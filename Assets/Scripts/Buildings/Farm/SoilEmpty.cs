using UnityEngine;

namespace Buildings.Farm
{
    public class SoilEmpty : IFarmingState
    {

        private Character _farmer;
        private MeshRenderer _dirt;
        
        public void Start(Farm owner)
        {
            Transform trans = owner.transform;
            _dirt = trans.Find("Dirt").GetComponent<MeshRenderer>();

           _dirt.enabled = false;
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
            Debug.Log(_dirt);
            _dirt.enabled = true;
        }
    }
}