using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class BuildWalkway : MonoBehaviour
{
    private GameObject _entity;
    private NavMeshSurface _navMeshSurface;

    public GameObject walkwayGameObject;
    public Material newMaterial;


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
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                RaycastHit hit;

                // Use a raycast (range 100) to register the position of the mouse
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                    GameObject entity = PrefabInstanceManager.Instance.Spawn(
                        this.walkwayGameObject,
                        new Vector3(hit.point.x, 0, hit.point.z),
                        Quaternion.Euler(0, 90, 0)
                        );
                    this._entity = entity;
                }
            }
            this._entity.transform.parent = this.transform;
            //MeshRenderer gameObjectRenderer = entity.GetComponent<MeshRenderer>();
            //gameObjectRenderer.material = newMaterial ;
        }

        if (this._entity != null)
        {
            UpdateDockPosition();

            if (Input.GetButtonDown("Cancel"))
            {
                PrefabInstanceManager.Instance.DestroyEntity(this._entity.GetInstanceID());
                this._entity = null;
            }

            //if (Input.GetButtonDown("KeyRotateRight"))
            //    _entity.transform.Rotate(0, 90, 0);

            //if (Input.GetButtonDown("KeyRotateLeft"))
            //    _entity.transform.Rotate(0, -90, 0);

            if (Input.GetKeyDown(KeyCode.U))
            {
                this._entity = null;
                this._navMeshSurface.BuildNavMesh();
            }
        }
    }

    private void UpdateDockPosition()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit hit;

            // Use a raycast (range 100) to register the position of the mouse
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                _entity.transform.position = new Vector3(hit.point.x, 0, hit.point.z);
            }
        }
    }
}

//PrefabInstanceManager.Instance.DestroyEntity(this._entity.GetInstanceID());
//                    this._entity = null;
//                    this._navMeshSurface.BuildNavMesh();