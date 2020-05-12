using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("RightMouseButton"))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                RaycastHit hit;

                bool shift = Input.GetKey(KeyCode.LeftShift) 
                             || Input.GetKey(KeyCode.RightShift);
                
                // Use a raycast (range 100) to register the position of the mouse click and set the agents new destination
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                    foreach (Selectable current in SelectionController.selectedEntities)
                    {
                        switch (current.name)
                        {
                            case "Character":
                                GlobalActions.CharacterDoAction(hit.point, 
                                                                current.GetComponent<Character>(), 
                                                                hit.collider.gameObject.name, shift);
                                break;
                            case "Boat":
                                
                                break;
                            default:
                                break;
                        }
                    }
                    // switch (hit.collider.gameObject.name)
                    // {
                    //     case "Walkway":
                    //         foreach (Selectable current in SelectionController.selectedEntities)
                    //         {
                    //             if (current.name == "Character")
                    //             {
                    //                 Character character = current.GetComponent<Character>();
                    //                 
                    //                 if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                    //                     character.Brain.AddSubGoal(new MoveTo(character, hit.point));
                    //                 else
                    //                     character.Brain.PrioritizeSubGoal(new MoveTo(character, hit.point));
                    //
                    //             }
                    //         }
                    //         break;
                    //     case "Character":
                    //         
                    //         break;
                    //     default:
                    //         break;
                    // }


                }
            }
        }
    }
}
