using Resources;
using UnityEngine;

class Amass : IGoal
{
    private Boat _owner;

    public GoalStatus Status { get; private set; }

    public string Name { get; private set; }

    private float _time;
    private int _accumulateAmount;

    public Amass(Boat owner, float duration)
    {
        _owner = owner;
        _time = duration;
    }

    public void Activate()
    {
        _accumulateAmount = (int)Random.Range(0, _owner.MaxCarrierAmount);

        Renderer[] rends = _owner.gameObject.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < rends.Length; i++)
            rends[i].enabled = false;

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

        Renderer[] rends = _owner.gameObject.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < rends.Length; i++)
            rends[i].enabled = true;
        
        Status = GoalStatus.Completed;
    }

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