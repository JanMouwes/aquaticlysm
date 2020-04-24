using System;
using UnityEngine;
using UnityEngine.AI;

public class SpawnWalkway : MonoBehaviour
{
    private Tuple<int, GameObject> _gameObjectTuple;
    private NavMeshSurface         _navMeshSurface;
    public  GameObject             walkwayGameObject;

    // Start is called before the first frame update
    private void Awake()
    {
        _navMeshSurface = GetComponent<NavMeshSurface>();
        _navMeshSurface.BuildNavMesh();
    }

    // Update is called once per frame
    private void Update()
    {
        if (_gameObjectTuple == null && Input.GetKeyDown(KeyCode.Y))
        {
            _gameObjectTuple = SpawnController.Instance.SpawnTuple(walkwayGameObject, new Vector3(8.5f, 0, 0),
                                                                   Quaternion.Euler(0, 90, 0));
            _gameObjectTuple.Item2.transform.parent = transform;
            _navMeshSurface.BuildNavMesh();
        }

        if (_gameObjectTuple != null && Input.GetKeyDown(KeyCode.U))
        {
            SpawnController.Instance.DestroyGameObject(_gameObjectTuple.Item1);
            _gameObjectTuple = null;
            _navMeshSurface.BuildNavMesh();
        }
    }
}