using UI;
using UnityEngine;

public class CharacterSystem : MonoBehaviour
{
    public CharacterManager CharacterManager { get; set; }

    public GameObject characterPrefab;

    public GameObject entitiesGameObject;

    public CharacterSystem()
    {
        CharacterManager = new CharacterManager();
    }

    private void Start()
    {
        for (int i = 0; i < 7; i++)
        {
            SpawnCharacter();
        }
    }

    private void SpawnCharacter()
    {
        GameObject instance = PrefabInstanceManager.Instance.Spawn(this.characterPrefab, new Vector3(0,3,0));
        Character character = instance.GetComponent<Character>();
        
        character.Portrait = UnityEngine.Resources.Load<Sprite>("Sprites/John");

        instance.transform.parent = this.entitiesGameObject.transform;
    }
}