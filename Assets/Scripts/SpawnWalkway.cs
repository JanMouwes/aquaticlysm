using System;
using UnityEngine;
using UnityEngine.AI;

public class SpawnWalkway : MonoBehaviour
{
    private GameObject _entity;
    private NavMeshSurface _navMeshSurface;
    public GameObject walkwayGameObject;

    // Start is called before the first frame update
    private void Awake()
    {
        this._navMeshSurface = GetComponent<NavMeshSurface>();
        this._navMeshSurface.BuildNavMesh();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y) && this._entity == null)
        {
            GameObject entity = PrefabInstanceManager.Instance.Spawn(
                this.walkwayGameObject,
                new Vector3(8.5f, 0, 0),
                Quaternion.Euler(0, 90, 0)
            );

            this._entity = entity;

            this._entity.transform.parent = this.transform;
            this._navMeshSurface.BuildNavMesh();
        }

        if (Input.GetKeyDown(KeyCode.U) && this._entity != null)
        {
            PrefabInstanceManager.Instance.DestroyEntity(this._entity.GetInstanceID());
            this._entity = null;
            this._navMeshSurface.BuildNavMesh();
        }
    }
}