using System;
using UnityEngine;


public class CharacterSystem
{
    
    public CharacterManager CharacterManager { get; set; }

    public CharacterSystem()
    {
        CharacterManager = new CharacterManager();
    }
    
}