using System;
using Resources;
using UnityEngine;
using UnityEngine.Serialization;

namespace Buildings.Energy
{
    public class Battery : MonoBehaviour
    {
        private EnergySystem _energySystem;

        public int maximumCharge = 500;

        private void Start()
        {
            _energySystem = GameObject.Find("EnergySystem").GetComponent<EnergySystem>();
        }

        private void OnEnable()
        {
            // Add maximumcharge to EnergySystem when object is enabled
            EnergySystem.MaximumCharge += this.maximumCharge;
        }

        private void OnDisable()
        {
            // Subtract the energy amount from the total through the EnergySystem
            _energySystem.SubtractBatteryCharge(this.maximumCharge);
        }
    }
}