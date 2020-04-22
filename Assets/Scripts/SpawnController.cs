using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SpawnController : MonoBehaviour
{
    //private List<GameObject> entities;
    private Dictionary<string, GameObject> entities;
    public static SpawnController Instance;
    private void Awake()
    {
        //entities = new List<GameObject>();
        Instance = this;
        entities = new Dictionary<string, GameObject>();
    }

    

    int i=0;
    public string Spawn(GameObject gameObject, Vector3 spawnPosition, Quaternion rotation)
    {
        GameObject temp = Instantiate(gameObject, spawnPosition, rotation);
        string Objectname = temp.name + i;
        entities.Add(Objectname, temp);
        temp.name = Objectname;
        i++;
        return Objectname;
    }

    public GameObject GetGameObject(string Name){
        return entities[Name];
    }

    public string Spawn(GameObject gameObject, Vector3 spawnPosition){
        return Spawn(gameObject,spawnPosition,new Quaternion(0, 0, 0, 1));
    }

    public void DestroyGameObject(string Name)
    {
        Destroy(entities[Name]);
        entities.Remove(Name);
    }
}
