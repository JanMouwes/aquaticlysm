using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalActions
{
    public static void CharacterDoAction(Vector3 clickPosition, 
                               Character character,
                               string targetName,
                               bool shiftClick)
    {
        switch (targetName)
        {
            case "Walkway":
                if (shiftClick)
                    character.Brain.AddSubGoal(new MoveTo(character, clickPosition));
                else
                    character.Brain.PrioritizeSubGoal(new MoveTo(character, clickPosition));
                
                break;
            case "RestPlace":
                if (shiftClick)
                    character.Brain.AddSubGoal(new Rest(character, clickPosition));
                else
                    character.Brain.PrioritizeSubGoal(new Rest(character, clickPosition));
                break;
            case "Boat":
                Debug.Log("Booty");
                break;
            default:
                break;
        }
    }
}
