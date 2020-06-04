using UnityEngine;

namespace Buildings.Farm.States
{
    public class FullGrown : FarmingState
    {
        private GameObject _growthPhase;

        public override void Start(Farm owner)
        {
            this._growthPhase = owner.transform.Find("GrowthPhase5").gameObject;
        }

        public override void Stop(Farm owner)
        {
            this._growthPhase.SetActive(false);
        }
    }
}