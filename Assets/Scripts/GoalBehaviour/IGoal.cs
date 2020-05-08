/// <summary>
///     Base class for Goal Driven Behaviour
/// </summary>
public interface IGoal
{
    // Status to keep a record of the status of the goal
    Character Owner { get; }
    GoalStatus Status { get; }
    string Name { get; }
    void Activate();

    GoalStatus Process();

    void Terminate();
}

/// <summary>
///     For keeping track of the completion status of goals
/// </summary>
public enum GoalStatus
{
    Inactive,
    Active,
    Completed,
    Failed
}