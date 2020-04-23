using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class SpawnWalkway : MonoBehaviour
{
    public GameObject WalkwayGameObject;
    private NavMeshSurface navMeshSurface;
    // Start is called before the first frame update
    void Awake()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
        navMeshSurface.BuildNavMesh();
       
    }

    private Tuple<int, GameObject> _gameObjectTuple;
    // Update is called once per frame
    void Update()
    {
        if (_gameObjectTuple == null && Input.GetKeyDown(KeyCode.Y))
        {
            _gameObjectTuple = SpawnController.Instance.SpawnTuple(WalkwayGameObject, new Vector3(8.5f, 0, 0),
                                                Quaternion.Euler(0, 90, 0));
            _gameObjectTuple.Item2.transform.parent = transform;
            navMeshSurface.BuildNavMesh();
        }

        if (_gameObjectTuple != null && Input.GetKeyDown(KeyCode.U))
        {
            SpawnController.Instance.DestroyGameObject(_gameObjectTuple.Item1);
            _gameObjectTuple = null;
            navMeshSurface.BuildNavMesh();
        }
    }
}
