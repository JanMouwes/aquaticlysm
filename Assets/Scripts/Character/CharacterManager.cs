using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class CharacterManager
{
    public IEnumerable<Character> characters => SelectionController.selectables
                                                                   .Select(selectable => selectable.gameObject.GetComponent<Character>())
                                                                   .Where(character => character != null);

    public List<Character> list;
    public CharacterManager()
    {
        list = characters.ToList();
    }

    public void UpdateList()
    {
        list = characters.ToList();
    }

    public void PrintList()
    {
        foreach (Character character in list)
        {
            Debug.Log(character.FirstName + " " + character.LastName);
        }
    }
}
