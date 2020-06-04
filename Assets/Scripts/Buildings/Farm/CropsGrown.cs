using Resources;
using UnityEngine;

namespace Buildings.Farm
{
    public class CropsGrown : IFarmingState
    {
        private Character _farmer;
        private MeshRenderer _dirt;

        public void Start(Farm owner)
        {
            Transform trans = owner.transform;
            _dirt = trans.Find("Dirt").GetComponent<MeshRenderer>();

            _dirt.material.color = Color.green;
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
            _dirt.material.color = new Color(139,69,19);
        }
    }
}