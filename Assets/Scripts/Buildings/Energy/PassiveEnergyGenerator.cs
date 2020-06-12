using System;
using Resources;
using UnityEngine;

namespace Buildings.Energy
{
    
    public class PassiveEnergyGenerator : MonoBehaviour
    {
        private EnergySystem _energySystem;
        private readonly int _maximumCharge = 20;
        private bool completed;
        public int chargeAmount = 10;
        

        private void Start()
        {
            _energySystem = GameObject.Find("EnergySystem").GetComponent<EnergySystem>();
        }

        private void Update()
        {
            if (completed)
                return;
            if (!GetComponent<DissolveController>().CheckifBuild())
                return;
            completed = true;
            OnEnable();
        }

        private void OnEnable()
        {
            if(!completed)
                return;
            EnergySystem.ChargeStepAmount += chargeAmount;
            // Add maximumcharge to EnergySystem when object is enabled
            EnergySystem.MaximumCharge += _maximumCharge;
        }

        private void OnDisable()
        {
            if(!completed)
                return;
            EnergySystem.ChargeStepAmount -= this.chargeAmount;
            // Subtract the energy amount from the total through the EnergySystem
            _energySystem.DecreaseMaximumCharge(_maximumCharge);
        }
        
        
    }
}