using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton class that contains a dictionary with all possible resources.
///
/// Fetch the instance with 'ResourceManager.Instance'
/// </summary>
public class ResourceManager
{
    private static ResourceManager _instance = null;
    private readonly Dictionary<string, int> _resources = new Dictionary<string, int>();

    /// <summary>
    /// Fetch the single instance
    /// </summary>
    public static ResourceManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new ResourceManager();

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
        if (neededAmount < 0)
            return false;

        if (CanSubtractResourceAmount(resource, neededAmount))
        {
            _resources[resource] -= neededAmount;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Adds a completely new resource to the resources dictionary
    /// if does not yet exist.
    /// </summary>
    /// <param name="resource"></param>
    /// <param name="amount">Optional</param>
    public void AddNewResource(string resource, int amount = 0)
    {
        if (!_resources.ContainsKey(resource))
            _resources.Add(resource, amount);
    }
    
    /// <summary>
    /// Clears the entire dictionary of all its resources.
    /// </summary>
    public void Clear() => _resources.Clear();
    
    /// <summary>
    /// Return the resource amount corresponding to the key.
    /// If the resource does not exist yet, it gets added.
    /// </summary>
    /// <param name="resource"></param>
    /// <returns>The amount of resources </returns>
    public int GetResourceAmount(string resource)
    {
        if (_resources.TryGetValue(resource, out int value))
            return value;

        AddNewResource(resource, 0);
        return 0;
    }

    /// <summary>
    /// Remove a certain resource from the dictionary
    /// </summary>
    /// <param name="resource"></param>
    public void RemoveResource(string resource)
    {
        _resources.Remove(resource);
    }

    /// <summary>
    /// Check if string exists within dictionary
    /// </summary>
    /// <param name="resource"></param>
    /// <returns>true if key exists, false if key doesn't exist</returns>
    public bool DoesResourceExist(string resource)
    {
        if (_resources.ContainsKey(resource))
            return true;

        return false;
    }
    
    /// <summary>
    /// Check if there's enough of a certain resource
    /// </summary>
    /// <param name="resource"></param>
    /// <param name="amount"></param>
    /// <returns>true if there's enough resources</returns>
    private bool CanSubtractResourceAmount(string resource, int amount)
    {
        if (_resources.ContainsKey(resource))
            return _resources[resource] >= amount;

        return false;
    }

}