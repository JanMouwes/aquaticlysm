using UnityEngine;

namespace Buildings.Farm
{
    public class GrowCycle : IFarmingState
    {
        private float _time;
        private MeshRenderer _dirt;

        public void Start(Farm owner)
        {
            _time = 4f;
            
            Transform trans = owner.transform;
            _dirt = trans.Find("Dirt").GetComponent<MeshRenderer>();

            _dirt.material.color = Color.yellow;
        }

        public void Execute(Farm owner)
        {
            _time -= Time.deltaTime;

            if (_time <= 0)
                owner.ChangeState(new CropsGrown());
        }

        public void Stop(Farm owner)
        {
            
        }
    }
}