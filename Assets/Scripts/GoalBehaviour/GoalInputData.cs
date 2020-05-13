using UnityEngine;

public struct GoalInputData
{
    public Character Owner { get; private set; }
    public Vector3 Position { get; set; }

    public GoalInputData(Character character)
    {
        this.Owner = character;
        this.Position = Vector3.zero;
    }
}
