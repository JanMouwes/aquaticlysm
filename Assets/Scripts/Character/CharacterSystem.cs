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
        GameObject instance = PrefabInstanceManager.Instance.Spawn(this.characterPrefab, Vector3.zero);
        Character character = instance.GetComponent<Character>();

        character.Portrait = UnityEngine.Resources.Load<Sprite>("Sprites/John");

        instance.transform.parent = this.entitiesGameObject.transform;
    }
}