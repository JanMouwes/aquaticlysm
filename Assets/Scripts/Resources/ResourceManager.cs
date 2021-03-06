﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Resources
{
    /// <summary>
    /// Singleton class that contains a dictionary with all possible resources.
    ///
    /// Fetch the instance with 'ResourceManager.Instance'
    /// </summary>
    public class ResourceManager : MonoBehaviour
    {
        private readonly Dictionary<string, Resource> _resources = new Dictionary<string, Resource>();
        private IResourceTypeManager _resourceTypeManager;

        public IEnumerable<Resource> Resources => this._resources.Values;

        private static ResourceManager _instance;

        public static ResourceManager Instance { get { return _instance; } }


        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(_instance.gameObject);
            } else {
                _instance = this;
            }
        }

        /// <summary>
        /// Adds a particular resource to the resources.
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="amount"></param>
        public void IncreaseResource(string resource, int amount)
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
        /// <param name="amount"></param>
        /// <returns>Whether amount can be decreased</returns>
        public bool DecreaseResource(string resource, int amount)
        {
            if (amount < 0)
                return false;

            if (CanSubtractResourceAmount(resource, amount))
            {
                this._resources[resource].Remove(amount);

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
                if (!this._resourceTypeManager.TryGetResourceType(resourceName, out ResourceType type))
                {
                    // Resource not registered
                    throw new SystemException($"Resource '{resourceName}' is non-existent inside {nameof(ResourceTypeManager)}.");
                }

                Resource resource = new Resource(type, initialAmount);
                this._resources.Add(resourceName, resource);
            }
        }

        public void SetResourceTypeManager(IResourceTypeManager resourceTypeManager)
        {
            this._resourceTypeManager = resourceTypeManager;
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
        /// Directly set the value of a resource.
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="amount"></param>
        /// <returns>true if key exists, false if key doesn't exist</returns>
        public bool SetResourceAmount(string resource, int amount)
        {
            if (!DoesResourceExist(resource))
                return false;

            this._resources[resource].Set(amount);

            return true;
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

        /// <summary>
        /// A simple algorithm to randomly pick a resource type based on the scarcity.
        /// </summary>
        /// <returns>A random resource type.</returns>
        public string GetRandomType()
        {
            int counter = 0;
            int scarcitySum = _resourceTypeManager.ResourceTypes.Select(t => t.Scarcity).Sum();
            string[] types = new string[scarcitySum];

            foreach (ResourceType type in _resourceTypeManager.ResourceTypes)
                for (int i = 0; i < type.Scarcity; i++, counter++)
                    types[counter] = type.ShortName;


            return types[UnityEngine.Random.Range(0, scarcitySum)];
        }
    }
}