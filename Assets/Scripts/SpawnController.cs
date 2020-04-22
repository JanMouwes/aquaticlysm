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

    public static SpawnController Instance;

    private void Awake()
    {
        Instance = this;
        _objects = new Dictionary<int, GameObject>();
        _iteration = 0;
    }
    /// <summary>
    /// Spawn a GameObject in the world at the designated position with a designated rotation.
    /// </summary>
    /// <param name="gameObject">The prefab to spawn</param>
    /// <param name="spawnPosition">Vector3 position to spawn the object</param>
    /// <param name="rotation">Start rotation at spawn.</param>
    /// <returns>Returns the index of the gameobject inside the container.</returns>
    public int Spawn(GameObject gameObject, Vector3 spawnPosition, Quaternion rotation)
    {
        GameObject temp = Instantiate(gameObject, spawnPosition, rotation);
        string Objectname = temp.name + "_" + _iteration;
        _objects.Add(_iteration, temp);
        temp.name = Objectname;
        int toReturn = _iteration;
        _iteration++;

        return toReturn;
    }

    /// <summary>
    /// Spawn a GameObject in the world at the designated position.
    /// </summary>
    /// <param name="gameObject">The prefab to spawn.</param>
    /// <param name="spawnPosition">Vector3 position to spawn the object.</param>
    /// <returns>Returns the index of the gameobject inside the container.</returns>
    public int Spawn(GameObject gameObject, Vector3 spawnPosition)
    {
        return Spawn(gameObject, spawnPosition, new Quaternion(0, 0, 0, 1));
    }

    /// <summary>
    /// Retrieve a GameObject that exists at a specific index.
    /// </summary>
    /// <param name="index"></param>
    /// <returns>A GameObject</returns>
    public GameObject GetGameObject(int index){
        return _objects[index];
    }

    /// <summary>
    /// Destroy a GameObject inside the world and inside the container.
    /// </summary>
    /// <param name="index"></param>
    public void DestroyGameObject(int index)
    {
        Destroy(_objects[index]);
        _objects.Remove(index);
    }
}
