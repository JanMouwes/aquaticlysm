using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [System.Serializable]
    public class BuildingCosts
    {
        public string resourceName;
        public int resourceAmount;
    }

    public BuildingCosts[] BuildingCostsList;
}

