using Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBarrel : IGoal
{
    private Boat _owner;
    public GoalStatus Status { get; private set; }
    public string Name { get; private set; }

    private float _time;
    private int _accumulateAmount;
    private GameObject _barrel;
    public OpenBarrel(Boat owner, GameObject gameObject, float duration)
    {
        Name = "AccumulateResources";
        _owner = owner;
        _time = duration;
        _barrel = gameObject;
    }

    public void Activate()
    {
        // The random amount of stuff it wil accumulate.
        _accumulateAmount = (int)Random.Range(0, _owner.maxCarrierAmount);

        Status = GoalStatus.Active;
    }

    public GoalStatus Process()
    {
        if (Status == GoalStatus.Inactive)
            Activate();

        _time -= Time.deltaTime;

        if (_time <= 0)
            Terminate();

        return Status;
    }

    public void Terminate()
    {
        AccumulateRandomResource(_owner, _accumulateAmount);
        ScrapBarrel(_owner);
        GameObject.Destroy(_barrel);

        Status = GoalStatus.Completed;
    }

    /// <summary>
    /// Fills the boat with random resources.
    /// </summary>
    /// <param name="boat">The boat where it will save the resources.</param>
    /// <param name="amount">The total amount of resources.</param>
    public static void AccumulateRandomResource(Boat boat, int amount)
    {
        string type = ResourceManager.Instance.GetRandomType();
        float resourceAmount = boat.TryGetResourceValue(type);
        boat.carriedResources[type] = resourceAmount + amount;
    }

    public static void ScrapBarrel(Boat boat)
    {
        float resourceAmount = boat.TryGetResourceValue("metal");
        boat.carriedResources["metal"] = resourceAmount + 5;
    }
}