using UnityEngine;

namespace Buildings.Farm
{
    public class GrowCycle : IFarmingState
    {
        private float _time; 
        public void Start(Farm owner)
        {
            _time = 15f;
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