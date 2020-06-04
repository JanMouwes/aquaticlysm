using System;
using System.Collections.Generic;
using Buildings.Energy;
using UnityEngine;

namespace Resources
{
    public class EnergySystem : MonoBehaviour
    {

        public static int MaximumCharge = 0;
        public static int ChargeStepAmount = 0;
        
        private ResourceManager _resourceManager;
        private float _timeStep;

        private void Start()
        {
            _resourceManager = ResourceManager.Instance;
        }

        private void Update()
        {
            if (_timeStep > 1)
            {
                if(DateTime.IsDay())
                    UpdateEnergyAmount();
                
                _timeStep = 0f;
            }
            else
            {
                _timeStep += 1 * Time.deltaTime;
            }
        }

        private void UpdateEnergyAmount()
        {
            int stepUpdateAmount = ChargeStepAmount;

            int currentAmount = _resourceManager.GetResourceAmount("energy");
            
            if (stepUpdateAmount + currentAmount > MaximumCharge)
            {
                stepUpdateAmount = MaximumCharge - currentAmount;
            }
            
            _resourceManager.IncreaseResource("energy", stepUpdateAmount);
            Debug.Log("Step Update: " + stepUpdateAmount + "/" + MaximumCharge);
        }
        
    }
}