using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource
{
    public int Amount { get; private set; }
    public readonly ResourceType type;
    
    public Resource(ResourceType type, int amount)
    {
        this.type = type;
        this.Amount = amount;
    }

    /// <summary>
    /// Adds a specified amount of resources 
    /// </summary>
    /// <param name="amount"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void Add(int amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Parameter has negative value.");

        this.Amount += amount;
    }

    /// <summary>
    /// Removes a specified amount of resources
    /// </summary>
    /// <param name="amount"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void Remove(int amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Parameter has negative value.");

        this.Amount -= amount;
    }
}
