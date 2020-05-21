using UnityEngine;

public struct GoalCommand
{ 
    public Character Owner { get; private set; }

    public Vector3 Position { get; set; }
    
    public GameObject Building { get; set;}

    public GoalCommand(Character character)
    {
        this.Owner = character;
        this.Position = Vector3.zero;
        this.Building = null;
    }
}
