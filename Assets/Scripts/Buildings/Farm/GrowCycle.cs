using UnityEngine;

namespace Buildings.Farm
{
    public class GrowCycle : IFarmingState
    {
        private float _time;
        private GameObject _oldGrowthPhase;
        private GameObject _growthPhase;

        public void Start(Farm owner)
        {
            _time = 4f;

            Transform trans = owner.transform;
            _oldGrowthPhase = trans.Find("GrowthPhase1").gameObject;
            _growthPhase = trans.Find("GrowthPhase5").gameObject;

        }

        public void Execute(Farm owner)
        {
            _time -= Time.deltaTime;

            if (_time <= 0)
                owner.ChangeState(new CropsGrown());
        }

        public void Stop(Farm owner)
        {
            _oldGrowthPhase.SetActive(false);
            _growthPhase.SetActive(true);
        }
    }
}