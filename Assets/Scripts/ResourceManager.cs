using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class ResourceManager 
{
    private static ResourceManager _instance = null;
    private readonly Dictionary<string, int> _resources = new Dictionary<string, int>();
    
    public static ResourceManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ResourceManager();
            }

            return _instance;
        }
    }


    /// <summary>
    /// Adds a particular resource to the resources.
    /// </summary>
    /// <param name="resource"></param>
    /// <param name="amount"></param>
    public void GatherResource(string resource, int amount)
    {
        if (_resources.ContainsKey(resource))
            _resources[resource] += amount;
        else
            _resources.Add(resource, amount);
        
        if (_resources[resource] < 0)
            _resources[resource] = 0;
    }

    /// <summary>
    /// Checks if there's enough of a particular resource and subtracts it.
    /// </summary>
    /// <param name="resource"></param>
    /// <param name="neededAmount"></param>
    /// <returns></returns>
    public bool UseResource(string resource, int neededAmount)
    {
        if (CheckForResource(resource, neededAmount))
        {
            _resources[resource] -= neededAmount;
            return true;
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="resource"></param>
    /// <param name="amount"></param>
    public void AddNewResource(string resource, int amount = 0)
    {
        if (!_resources.ContainsKey(resource))
        {
            _resources.Add(resource, amount);
        }
    }
    /// <summary>
    /// Check if there's enough of a certain resource
    /// </summary>
    /// <param name="resource"></param>
    /// <param name="amount"></param>
    /// <returns>true if there's enough resources</returns>
    private bool CheckForResource(string resource, int amount)
    {
        if (_resources.ContainsKey(resource))
        {
            if (_resources[resource] >= amount)
                return true;
            
            return false;
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="resource"></param>
    /// <returns></returns>
    public int GetResourceAmount(string resource)
    {
        if (_resources.ContainsKey(resource))
        {
            return _resources[resource];
        }

        return 0;
    }

    public void Clear()
    {
        _resources.Clear();
    }
}







