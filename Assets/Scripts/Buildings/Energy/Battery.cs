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
            EnergySystem.MaximumCharge += _maximumCharge;
        }

        private void OnDisable()
        {
            _energySystem.SubtractBatteryCharge(_maximumCharge);
        }
    }
}