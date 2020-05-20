using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walkway : Selectable
{
    
    public override bool ActionHandler(string tag, Vector3 position, bool priority)
    {
        return false;
    }
}
