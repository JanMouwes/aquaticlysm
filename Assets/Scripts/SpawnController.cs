using System;
using System.Collections.Generic;
using Entity;
using UnityEngine;

[Serializable]
public class SpawnController : MonoBehaviour
{
    private EntitySystem _entitySystem;

    public static SpawnController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        // Save the entity system to avoid continuous retrieval
        this._entitySystem = EntitySystem.Instance;
    }

    /// <summary>
    ///     Spawn a GameObject in the world at the designated position with a designated rotation.
    /// </summary>
    /// <param name="prefab">The prefab to spawn</param>
    /// <param name="spawnPosition">Vector3 position to spawn the object</param>
    /// <param name="startRotation">Start rotation at spawn.</param>
    /// <returns>New entity's id</returns>
    public int Spawn(GameObject prefab, Vector3 spawnPosition, Quaternion startRotation)
    {
        GameObject entity = Instantiate(prefab, spawnPosition, startRotation);

        return this._entitySystem.RegisterEntity(entity, prefab.name);
    }

    public (int id, GameObject entity) SpawnTuple(GameObject prefab, Vector3 spawnPosition, Quaternion startRotation)
    {
        int id = Spawn(prefab, spawnPosition, startRotation);
        GameObject go = GetGameObject(id);

        return (id, go);
    }

    /// <summary>
    ///     Spawn a GameObject in the world at the designated position.
    /// </summary>
    /// <param name="gameObject">The prefab to spawn.</param>
    /// <param name="spawnPosition">Vector3 position to spawn the object.</param>
    /// <returns>Returns the index of the gameobject inside the container.</returns>
    public int Spawn(GameObject gameObject, Vector3 spawnPosition)
    {
        return Spawn(gameObject, spawnPosition, Quaternion.identity);
    }

    /// <summary>
    ///     Retrieve a GameObject that exists at a specific index.
    /// </summary>
    /// <param name="id">ID integer that belongs to the selected GameObject</param>
    /// <returns>A GameObject</returns>
    public GameObject GetGameObject(int id) => this._entitySystem.GetEntity(id);

    /// <summary>
    ///     Destroy a GameObject inside the world and inside the container.
    /// </summary>
    /// <param name="id">ID integer that belongs to the selected GameObject</param>
    public void DestroyGameObject(int id)
    {
        GameObject entity = GetGameObject(id);
        DestroyImmediate(entity);
        this._entitySystem.RemoveEntity(id);
    }
}