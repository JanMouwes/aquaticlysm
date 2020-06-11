using System;
using UnityEngine;

namespace Buildings
{
    /// <summary>
    /// Script for holding the costs of the building. BuildingCosts[] string nameOfTheResource, int amountOfResourceCosts.
    /// </summary>
    public class Building : MonoBehaviour
    {
        

        /// <summary>
        /// Add resource costs in the Unity Inspector when creating a new prefab.
        /// </summary>
        [System.Serializable]
        public class Costs
        {
            public string name;
            public int amount;
        }

        public Costs[] buildingCosts;
    }
}