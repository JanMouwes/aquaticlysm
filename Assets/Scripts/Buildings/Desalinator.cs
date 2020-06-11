using System;
using Resources;
using UnityEngine;

namespace Buildings
{
    public class Desalinator : MonoBehaviour
    {
        public int energyCost = 10;
        public int resourceIncrease = 25;
        public const int MaxStages = 5;
        
        private float _timeStep = 1f;
        private int _stage;

        private void Start()
        {
            _stage = MaxStages;
        }

        private void Update()
        {
            if (!GetComponent<DissolveController>().CheckifBuild())
                return;
            
            // Only update the energy amount once every predefined timestep
            if (_timeStep > 1)
            {
                Work();
                
                _timeStep = 0f;
            }
            else
            {
                _timeStep += 1 * Time.deltaTime;
            }
        }

        /// <summary>
        /// Decrease energy every defined 'stage'. After all stages are finished, Increase the water resource.
        /// </summary>
        private void Work()
        {
            if (ResourceManager.Instance.DecreaseResource("energy", energyCost))
            {
                _stage--;
            }

            if (_stage < 1)
            {
                ResourceManager.Instance.IncreaseResource("water", resourceIncrease);
                _stage = MaxStages;
            }
        }
        
    }
}