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
        Name = "Amass";
        _owner = owner;
        _time = duration;
        _renderers = _owner.gameObject.GetComponentsInChildren<Renderer>();
    }

    public void Activate()
    {
        // The random amount of stuff it wil accumulate.
        _accumulateAmount = (int)Random.Range(0, _owner.MaxCarrierAmount);

        // Make the boat invisible.
        for (int i = 0; i < _renderers.Length; i++)
            _renderers[i].enabled = false;

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
        for (int i = 0; i < _renderers.Length; i++)
            _renderers[i].enabled = true;
        
        Status = GoalStatus.Completed;
    }

    /// <summary>
    /// Fills the boat with random resources.
    /// </summary>
    /// <param name="boat">The boat where it will save the resources.</param>
    /// <param name="amount">The total amount of resources.</param>
    public static void AccumulateRandomResources(Boat boat, int amount) 
    {
        while (amount > 0) 
        {
            string type = ResourceManager.Instance.GetRandomType();
            int accumalationAmount = Random.Range(1, amount);

            float resourceAmount = boat.TryGetResourceValue(type);
            boat.CarriedResources[type] = accumalationAmount;

            amount -= accumalationAmount;
        }
    }
}