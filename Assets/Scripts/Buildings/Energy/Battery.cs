using System;
using Resources;
using UnityEngine;

namespace Buildings.Energy
{
    public class Battery : MonoBehaviour
    {
        private int _maximumCharge = 500;
        private EnergySystem _energySystem;
        
        private void Start()
        {
            _maximumCharge = 500;
            _energySystem = GameObject.Find("EnergySystem").GetComponent<EnergySystem>();
        }

        private void OnEnable()
        {
            // Add maximumcharge to EnergySystem when object is enabled
            EnergySystem.MaximumCharge += _maximumCharge;
        }

        private void OnDisable()
        {
            // Subtract the energy amount from the total through the EnergySystem
            _energySystem.SubtractBatteryCharge(_maximumCharge);
        }
    }
}