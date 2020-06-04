using UnityEngine;

namespace Buildings.Farm.States
{
    public class Growing : FarmingState
    {
        private GameObject _growthPhase;
        private float _time;
        private int _phase;

        private string[] _growthPhases =
        {
            "GrowthPhase1",
            "GrowthPhase2",
            "GrowthPhase3",
            "GrowthPhase4",
            "GrowthPhase5",
        };

        public override void Execute(Farm owner)
        {
            this._time -= Time.deltaTime;

            if (this._time <= 0)
            {
                if (this._phase == 4)
                    owner.ChangeState(new FullGrown());
                else
                    ChangePhase(owner);
            }
        }

        public override void Start(Farm owner)
        {
            this._phase = 1;
            this._time = 5f;
            this._growthPhase = owner.transform.Find(this._growthPhases[this._phase]).gameObject;
            this._growthPhase.SetActive(true);
        }

        private void ChangePhase(Farm owner)
        {
            this._phase++;
            this._time = 2f;
            this._growthPhase.SetActive(false);
            this._growthPhase = owner.transform.Find(this._growthPhases[this._phase]).gameObject;
            this._growthPhase.SetActive(true);
        }
    }
}