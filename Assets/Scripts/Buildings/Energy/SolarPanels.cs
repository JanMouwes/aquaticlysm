using System;
using Resources;
using UnityEngine;

namespace Buildings.Energy
{
    public class SolarPanels : MonoBehaviour
    {
        private int _chargeAmount = 10;
        

        private void OnEnable()
        {
            EnergySystem.ChargeStepAmount += _chargeAmount;
        }

        private void OnDisable()
        {
            EnergySystem.ChargeStepAmount -= _chargeAmount;
        }
        
    }
}