using UnityEngine;

namespace Buildings.Farm
{
    public class GrowCycle : IFarmingState
    {
        private GameObject _growthPhase;
        private float _time;
        private int _phase;

        private string[] _growthPhases = {
            "GrowthPhase1",
            "GrowthPhase2",
            "GrowthPhase3",
            "GrowthPhase4",
            "GrowthPhase5",
        };

        public void Execute(Farm owner)
        {
            _time -= Time.deltaTime;

            if (_time <= 0)
                if (_phase == 4)
                    owner.ChangeState(new CropsGrown());
                else
                    ChangePhase(owner);
        }

        public void Start(Farm owner)
        {
            _phase = 1;
            _time = 5f;
            _growthPhase = owner.transform.Find(_growthPhases[_phase]).gameObject;
            _growthPhase.SetActive(true);
        }

        public void Stop(Farm owner)
        {
        }

        private void ChangePhase(Farm owner)
        {
            _phase++;
            _time = 10f;
            _growthPhase.SetActive(false);
            _growthPhase = owner.transform.Find(_growthPhases[_phase]).gameObject;
            _growthPhase.SetActive(true);
        }
    }
}