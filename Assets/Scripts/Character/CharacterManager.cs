using System.Collections.Generic;
using System.Linq;


public class CharacterManager
{
    public IEnumerable<Character> characters => SelectionController.Selectables
                                                                   .Select(selectable => selectable.gameObject.GetComponent<Character>())
                                                                   .Where(character => character != null);
}
