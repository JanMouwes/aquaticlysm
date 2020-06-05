using System;
using Resources;
using UnityEngine;

namespace Buildings.Energy
{
    public class Battery : MonoBehaviour
    {
        private int _maximumCharge = 500;

        private void OnEnable()
        {
            EnergySystem.MaximumCharge += _maximumCharge;
        }

        private void OnDisable()
        {
            EnergySystem.SubtractBatteryCharge(_maximumCharge);
        }
    }
}