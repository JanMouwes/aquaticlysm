using System;
using UnityEngine;
using UnityEngine.AI;

public class SpawnWalkway : MonoBehaviour
{
    private GameObject _entity;
    private int _entityId;
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
            (int id, GameObject entity) = SpawnController.Instance.SpawnTuple(
                this.walkwayGameObject,
                new Vector3(8.5f, 0, 0),
                Quaternion.Euler(0, 90, 0)
            );

            this._entity = entity;
            this._entityId = id;

            this._entity.transform.parent = this.transform;
            this._navMeshSurface.BuildNavMesh();
        }

        if (Input.GetKeyDown(KeyCode.U) && this._entity != null)
        {
            SpawnController.Instance.DestroyGameObject(this._entityId);
            this._entity = null;
            this._navMeshSurface.BuildNavMesh();
        }
    }
}