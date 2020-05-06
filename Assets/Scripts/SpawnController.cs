using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class SpawnController : MonoBehaviour
{
    public static SpawnController Instance { get; private set; }

    private readonly Dictionary<int, GameObject> _gameObjectsById = new Dictionary<int, GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    ///     Spawn a GameObject in the world at the designated position with a designated rotation.
    /// </summary>
    /// <param name="prefab">The prefab to spawn</param>
    /// <param name="spawnPosition">Vector3 position to spawn the object</param>
    /// <param name="startRotation">Start rotation at spawn.</param>
    /// <returns>New entity's id</returns>
    public GameObject Spawn(GameObject prefab, Vector3 spawnPosition, Quaternion startRotation)
    {
        GameObject entity = Instantiate(prefab, spawnPosition, startRotation);

        int id = entity.GetInstanceID();

        this._gameObjectsById.Add(id, entity);

        return entity;
    }


    /// <summary>
    ///     Spawn a GameObject in the world at the designated position.
    /// </summary>
    /// <param name="prefab">The prefab to spawn.</param>
    /// <param name="spawnPosition">Vector3 position to spawn the object.</param>
    /// <returns>Returns the index of the gameobject inside the container.</returns>
    public GameObject Spawn(GameObject prefab, Vector3 spawnPosition)
    {
        return Spawn(prefab, spawnPosition, Quaternion.identity);
    }

    /// <summary>
    ///     Retrieve a GameObject according to its id.
    /// </summary>
    /// <param name="id">ID integer that belongs to the selected GameObject</param>
    /// <returns>A GameObject</returns>
    public GameObject GetEntity(int id) => this._gameObjectsById[id];

    /// <summary>
    ///     Destroy a GameObject inside the world and inside the container.
    /// </summary>
    /// <param name="id">ID integer that belongs to the selected GameObject</param>
    public void DestroyEntity(int id)
    {
        GameObject entity = GetEntity(id);
        DestroyImmediate(entity);
        this._gameObjectsById.Remove(id);
    }
}