using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Entity
{
    public class EntitySystem
    {
        /// <summary>
        /// Entities keyed by their ids. 
        /// </summary>
        private readonly Dictionary<int, (GameObject entity, string prefab)> _entities = new Dictionary<int, (GameObject entity, string prefab)>();

        private int _nextId = 1;

        private static EntitySystem instance;

        public static EntitySystem Instance
        {
            get
            {
                if (instance == null) { instance = new EntitySystem(); }

                return instance;
            }
        }

        /// <summary>
        /// All entities, tuple-bound with their ids
        /// </summary>
        public IEnumerable<(int id, GameObject entity)> Entities => this._entities.Select(kvp => (kvp.Key, kvp.Value.entity));

        private EntitySystem() { }

        /// <summary>
        /// Returns entity with id
        /// </summary>
        /// <param name="id">Entity's id</param>
        /// <returns>Entity if it's present</returns>
        /// <exception cref="IndexOutOfRangeException">When entity not found</exception>
        public GameObject GetEntity(int id)
        {
            if (this._entities.TryGetValue(id, out (GameObject entity, string prefab) tuple)) { return tuple.entity; }

            throw new IndexOutOfRangeException($"Entity with id {id} not found");
        }

        /// <summary>
        /// Removes entity. Does nothing if the entity does not exist.
        /// </summary>
        /// <param name="id">Entity's id</param>
        public void RemoveEntity(int id)
        {
            if (this._entities.ContainsKey(id)) { this._entities.Remove(id); }
        }

        /// <summary>
        /// Gets all entities of a specific prefab
        /// </summary>
        /// <param name="prefabName">Prefab's entities to retrieve</param>
        /// <returns>All entities tuple-bound with their ids</returns>
        public IEnumerable<(int id, GameObject entity)> GetPrefabEntities(string prefabName)
        {
            return this._entities
                       .Where(keyValue => keyValue.Value.prefab == prefabName)
                       .Select(pair => (pair.Key, pair.Value.entity));
        }

        /// <summary>
        /// Registers entity using a prefab-name 
        /// </summary>
        /// <param name="entity">Entity to register</param>
        /// <param name="prefabName">Name to register entity under</param>
        /// <returns>Entity's id</returns>
        public int RegisterEntity(GameObject entity, string prefabName)
        {
            this._entities.Add(this._nextId, (entity, prefabName));

            entity.name = $"{prefabName}_{this._nextId}";

            return this._nextId++;
        }
    }
}