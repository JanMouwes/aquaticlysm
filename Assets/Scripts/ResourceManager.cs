﻿using System;
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
    private readonly Dictionary<string, Resource> _resources = new Dictionary<string, Resource>();

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
        if (this._resources.ContainsKey(resource))
            this._resources[resource].Add(amount);
        else
            AddResourceType(resource, amount);
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
            this._resources[resource].Remove(neededAmount);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Adds a completely new resource to the resources dictionary
    /// if does not yet exist.
    /// </summary>
    /// <param name="resourceName"></param>
    /// <param name="initialAmount">Optional</param>
    public void AddResourceType(string resourceName, int initialAmount = 0)
    {
        if (!this._resources.ContainsKey(resourceName))
        {
            ResourceType type = new ResourceType(resourceName);
            Resource temp = new Resource(type, initialAmount);
            this._resources.Add(resourceName, temp);
        }
    }

    /// <summary>
    /// Clears the entire dictionary of all its resources.
    /// </summary>
    public void Clear() => this._resources.Clear();
    
    /// <summary>
    /// Return the resource amount corresponding to the key.
    /// If the resource does not exist yet, it gets added.
    /// </summary>
    /// <param name="resource"></param>
    /// <returns>The amount of resources </returns>
    public int GetResourceAmount(string resource)
    {
        if (this._resources.TryGetValue(resource, out Resource value))
            return value.Amount;

        AddResourceType(resource, 0);
        return 0;
    }

    /// <summary>
    /// Remove a certain resource from the dictionary
    /// </summary>
    /// <param name="resourceName"></param>
    public void RemoveResourceType(string resourceName)
    {
        this._resources.Remove(resourceName);
    }

    /// <summary>
    /// Check if string exists within dictionary
    /// </summary>
    /// <param name="resource"></param>
    /// <returns>true if key exists, false if key doesn't exist</returns>
    public bool DoesResourceExist(string resource)
    {
        return this._resources.ContainsKey(resource);
    }
    
    /// <summary>
    /// Check if there's enough of a certain resource
    /// </summary>
    /// <param name="resource"></param>
    /// <param name="amount"></param>
    /// <returns>true if there's enough resources</returns>
    private bool CanSubtractResourceAmount(string resource, int amount)
    {
        if (this._resources.ContainsKey(resource))
            return this._resources[resource].Amount >= amount;

        return false;
    }

}