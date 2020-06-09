using Resources;
using UnityEngine;

namespace Buildings.Energy
{
    public class PassiveEnergyGenerator : MonoBehaviour
    {
        public int chargeAmount = 10;

        private void OnEnable()
        {
            EnergySystem.ChargeStepAmount += this.chargeAmount;
        }

        private void OnDisable()
        {
            EnergySystem.ChargeStepAmount -= this.chargeAmount;
        }
    }
}