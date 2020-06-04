using System;
using Resources;
using UnityEngine;

namespace Buildings.Energy
{
    public class SolarPanels : MonoBehaviour
    {
        private int _chargeAmount = 10;
        private EnergySystem _energySystem;
        
        private void Start()
        {
            _energySystem = GameObject.Find("EnergySystem").GetComponent<EnergySystem>();
        }

        private void OnEnable()
        {
            EnergySystem.ChargeStepAmount += _chargeAmount;
        }

        private void OnDisable()
        {
            EnergySystem.ChargeStepAmount -= _chargeAmount;
        }
        
        public int GetChargeAmount()
        {
            // Only return energy if it is currently day...
            // return DateTime.IsDay() ? _chargeAmount : 0;
            return _chargeAmount;
        }


    }
}