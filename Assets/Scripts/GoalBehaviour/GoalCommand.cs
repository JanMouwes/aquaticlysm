using UnityEngine;

public struct GoalCommand<TOwner> where TOwner : MonoBehaviour
{ 
    public TOwner Owner { get; private set; }
    
    public Vector3 Position { get; set; }
    
    public GameObject Building { get; set;}

    public GoalCommand(TOwner owner)
    {
        this.Owner = owner;
        this.Position = Vector3.zero;
        this.Building = null;
    }
}
