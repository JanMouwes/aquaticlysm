using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SpawnController : MonoBehaviour
{
    // All of the spawned GameObjects
    private Dictionary<int, GameObject> _objects;
    // Current iteration of objects used as index
    private int _iteration;

    public static SpawnController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        _objects = new Dictionary<int, GameObject>();
        _iteration = 0;
    }
    /// <summary>
    /// Spawn a GameObject in the world at the designated position with a designated rotation.
    /// </summary>
    /// <param name="prefab">The prefab to spawn</param>
    /// <param name="spawnPosition">Vector3 position to spawn the object</param>
    /// <param name="startRotation">Start rotation at spawn.</param>
    /// <returns>Returns the index of the gameobject inside the container.</returns>
    public int Spawn(GameObject prefab, Vector3 spawnPosition, Quaternion startRotation)
    {
        GameObject temp = Instantiate(prefab, spawnPosition, startRotation);
        string objectName = temp.name + "_" + _iteration;
        _objects.Add(_iteration, temp);
        temp.name = objectName;
        int toReturn = _iteration;
        _iteration++;

        return toReturn;
    }

    public Tuple<int, GameObject> SpawnTuple(GameObject prefab, Vector3 spawnPosition, Quaternion startRotation)
    {
        int id = Spawn(prefab, spawnPosition, startRotation);
        GameObject go = GetGameObject(id);
        return new Tuple<int, GameObject>(id,go);
    }

    /// <summary>
    /// Spawn a GameObject in the world at the designated position.
    /// </summary>
    /// <param name="gameObject">The prefab to spawn.</param>
    /// <param name="spawnPosition">Vector3 position to spawn the object.</param>
    /// <returns>Returns the index of the gameobject inside the container.</returns>
    public int Spawn(GameObject gameObject, Vector3 spawnPosition)
    {
        return Spawn(gameObject, spawnPosition, Quaternion.identity);
    }

    /// <summary>
    /// Retrieve a GameObject that exists at a specific index.
    /// </summary>
    /// <param name="id">ID integer that belongs to the selected GameObject</param>
    /// <returns>A GameObject</returns>
    public GameObject GetGameObject(int id){
        return _objects[id];
    }

    /// <summary>
    /// Destroy a GameObject inside the world and inside the container.
    /// </summary>
    /// <param name="id">ID integer that belongs to the selected GameObject</param>
    public void DestroyGameObject(int id)
    {
        DestroyImmediate(_objects[id]);
        _objects.Remove(id);
    }
}
