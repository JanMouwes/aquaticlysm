using UnityEngine;

/// <summary>
///     Test goal for trying out goal behaviour. Meeting the energy need of the agent.
/// </summary>
public class Rest : IGoal
{
    public Character Owner { get; set; }
    public GoalStatus Status { get; set; }
    public string Name { get; set; }

    // Position of the resting place
    public Vector3 target;

    public Rest( Character owner)
    {
        this.Owner = owner;
    }
    
    public void Activate()
    {
        Status = GoalStatus.Active;
        target = GameObject.FindGameObjectWithTag("Rest").transform.position;
        Owner.agent.destination = target;
    }

    public GoalStatus Process()
    {
        //Debug.Log(Name);

        if (Status == GoalStatus.Inactive)
            Activate();

        // Terminating condition, if energylevel 100 the goal is met.
        if (isRested(Owner.energyLevel))
            Terminate();

        // Check, if the agent has arrived to the resting place
        if (Owner.agent.remainingDistance < 0.01f)
            Owner.energyLevel += 20 * Time.deltaTime;

        return Status;
    }

    public void Terminate()
    {
        Status = GoalStatus.Completed;
    }

    /// <summary>
    ///     Check, if energylevel has been met or not.
    /// </summary>
    /// <param name="energyLevel"></param>
    /// <returns></returns>
    private static bool isRested(float energyLevel)
    {
        return energyLevel >= 100.0f;
    }
}