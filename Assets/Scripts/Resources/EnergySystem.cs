﻿using System;
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
            // Only update the energy amount once every predefined timestep
            if (_timeStep > 1)
            {
                // Currently only call function while it's day
                if(DateTime.IsDay())
                    UpdateEnergyAmount();
                
                _timeStep = 0f;
            }
            else
            {
                _timeStep += 1 * Time.deltaTime;
            }
        }

        /// <summary>
        /// Decrease the MaximumCharge and change the amount of charge inside the ResourceManager
        /// </summary>
        /// <param name="amount"></param>
        public void DecreaseMaximumCharge(int amount)
        {
            ResourceManager instance = ResourceManager.Instance;
            int currentAmount = instance.GetResourceAmount("energy");
            int newAmount = MaximumCharge - amount;
            
            if (currentAmount > newAmount)
            {
                bool result = instance.SetResourceAmount("energy", newAmount);
            }

            MaximumCharge = newAmount;
        }
        
        /// <summary>
        /// Update the energy amount per timestep based on the amount of energy sources linked
        /// </summary>
        private void UpdateEnergyAmount()
        {
            int stepUpdateAmount = ChargeStepAmount;

            int currentAmount = ResourceManager.Instance.GetResourceAmount("energy");
            
            if (stepUpdateAmount + currentAmount > MaximumCharge)
            {
                stepUpdateAmount = MaximumCharge - currentAmount;
            }
            
            _resourceManager.IncreaseResource("energy", stepUpdateAmount);
        }
        
    }
}