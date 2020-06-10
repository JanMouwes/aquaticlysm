using Resources;
using UnityEngine;

public class AccumulateResources : IGoal
{
    private Boat _owner;
    public GoalStatus Status { get; private set; }
    public string Name { get; private set; }

    private float _time;
    private int _accumulateAmount;
    private Renderer[] _renderers;

    public AccumulateResources(Boat owner, float duration)
    {
        Name = "AccumulateResources";
        _owner = owner;
        _time = duration;
        _renderers = _owner.gameObject.GetComponentsInChildren<Renderer>();
    }

    public void Activate()
    {
        // The random amount of stuff it wil accumulate.
        _accumulateAmount = (int) Random.Range(0, _owner.maxCarrierAmount);

        // Make the boat invisible.
        foreach (Renderer renderer in this._renderers)
            renderer.enabled = false;

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
        AccumulateRandomResources(_owner, _accumulateAmount);

        // Make the boat visiable again.
        foreach (Renderer renderer in this._renderers)
            renderer.enabled = true;

        Status = GoalStatus.Completed;
    }

    /// <summary>
    /// Fills the boat with random resources.
    /// </summary>
    /// <param name="boat">The boat where it will save the resources.</param>
    /// <param name="amount">The total amount of resources.</param>
    public static void AccumulateRandomResources(Boat boat, int amount)
    {
        ResourceManager resourceManager = Object.FindObjectOfType<ResourceManager>();

        while (amount > 0)
        {
            string type = resourceManager.GetRandomType();
            int accumulationAmount = Random.Range(1, amount);

            boat.carriedResources[type] = accumulationAmount;

            amount -= accumulationAmount;
        }
    }
}