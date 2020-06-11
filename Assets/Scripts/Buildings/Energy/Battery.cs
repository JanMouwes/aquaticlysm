using System;
using Resources;
using UnityEngine;
using UnityEngine.Serialization;

namespace Buildings.Energy
{
    public class Battery : MonoBehaviour
    {
        private EnergySystem _energySystem;
        private bool completed;
        public int maximumCharge = 500;

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
            if (completed)
                return;
            // Add maximumcharge to EnergySystem when object is enabled
            EnergySystem.MaximumCharge += this.maximumCharge;
        }

        private void OnDisable()
        {
            if (completed)
                return;
            // Subtract the energy amount from the total through the EnergySystem
            _energySystem.DecreaseMaximumCharge(this.maximumCharge);
        }
    }
}