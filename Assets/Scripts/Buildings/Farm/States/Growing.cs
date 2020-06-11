using UnityEngine;

namespace Buildings.Farm.States
{
    public class Growing : FarmingState
    {
        private readonly string[] _growthPhases =
        {
            "GrowthPhase1",
            "GrowthPhase2",
            "GrowthPhase3",
            "GrowthPhase4",
            "GrowthPhase5",
        };

        private readonly float _totalGrowthTime;
        private GameObject _growthPhase;
        private float _time;
        private int _phase;

        public Growing(float growthTime) => this._totalGrowthTime = growthTime;

        public override void Start(Farm owner)
        {
            this._phase = 1;
            this._growthPhase = owner.transform.Find(this._growthPhases[this._phase]).gameObject;
            this._growthPhase.SetActive(true);
            
            // Start countdown
            this._time = this._totalGrowthTime / this._growthPhases.Length;
        }

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

        private void ChangePhase(Farm owner)
        {
            this._phase++;
            this._time = this._totalGrowthTime / this._growthPhases.Length;
            this._growthPhase.SetActive(false);
            this._growthPhase = owner.transform.Find(this._growthPhases[this._phase]).gameObject;
            this._growthPhase.SetActive(true);
        }
    }
}