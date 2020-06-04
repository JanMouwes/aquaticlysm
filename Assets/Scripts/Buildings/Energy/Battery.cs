using System;
using Resources;
using UnityEngine;

namespace Buildings.Energy
{
    public class Battery : MonoBehaviour
    {
        public int MaximumCharge { get; private set; }
        public int CurrentCharge { get; set; }

        private EnergySystem _energySystem;
        private void Start()
        {
            MaximumCharge = 1000;
            _energySystem = GameObject.Find("EnergySystem").GetComponent<EnergySystem>();
            
            EnergySystem.MaximumCharge += MaximumCharge;
            Debug.Log(_energySystem);
        }

        private void OnDisable()
        {
            EnergySystem.MaximumCharge -= MaximumCharge;
        }
    }
}