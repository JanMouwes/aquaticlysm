using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    private List<GameObject> entities;
    private void Awake()
    {
        entities = new List<GameObject>();
    }

    private int _index = -1;
    public int Spawn(GameObject gameObject, Vector3 spawnPosition, Quaternion rotation)
    {
        _index++;
        string Objectname = "test " + _index;
        entities.Add(Instantiate(gameObject, spawnPosition, rotation));
        entities[_index].name = Objectname;
        return _index;
    }

    public GameObject GetGameObject(int Index){
        return entities[Index];
    }

    public int Spawn(GameObject gameObject, Vector3 spawnPosition){
        return Spawn(gameObject,spawnPosition,new Quaternion(0, 0, 0, 1));
    }

    public void DestroyGameObject(int Index)
    {
        Destroy(entities[Index]);
        //entities.RemoveAt(Index);
    }
}
