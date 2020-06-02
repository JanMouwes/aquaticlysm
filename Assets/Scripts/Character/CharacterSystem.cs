using UI;
using UnityEngine;

public class CharacterSystem : MonoBehaviour
{
    public CharacterManager CharacterManager { get; set; }

    public GameObject characterPrefab;

    [Tooltip("The Entities object inside of the scene where Characters need to be a child of.")]
    public GameObject entitiesGameObject;

    public CharacterSystem()
    {
        CharacterManager = new CharacterManager();
    }

    private void Start()
    {
        // Spawn a number of Character instances.
        for (int i = 0; i < 7; i++)
        {
            SpawnCharacter();
        }
    }

    /// <summary>
    /// Spawn the first character entities and set a portrait.
    /// </summary>
    private void SpawnCharacter()
    {
        GameObject instance = PrefabInstanceManager.Instance.Spawn(this.characterPrefab, new Vector3(0,3,0));
        Character character = instance.GetComponent<Character>();
        
        character.Portrait = UnityEngine.Resources.Load<Sprite>("Sprites/character-default");

        instance.transform.parent = this.entitiesGameObject.transform;
    }
}