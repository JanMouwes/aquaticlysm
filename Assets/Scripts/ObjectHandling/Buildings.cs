using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using Util;

public class Buildings : MonoBehaviour
{
    private GameObject _currentEntity;
    private Outline _outline;
    public NavMeshSurface _navMeshSurface;
    public GameObject[] GameObjects;
    private GameObject prefab;
    private bool _walkable;

    // Start is called before the first frame update
    private void Awake()
    {
        GlobalStateMachine.instance.StateChanged += ToggleEnable;
        this._navMeshSurface = transform.GetChild(2).GetComponent<NavMeshSurface>();
        this._navMeshSurface.BuildNavMesh();
    }

    private void ToggleEnable(IState state) => this.enabled = state is Build;
    
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            ChangeBuildings(0);
            _walkable = true;
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            ChangeBuildings(1);
            _walkable = false;
        }

        if (Input.GetButtonDown("LeftMouseButton"))
        {
            UpdateCreateEntity();
        }
        

        UpdateCurrentEntity();
    }
    
    void ChangeBuildings(int index)
    {
        prefab = GameObjects[index];
    }


    /// <summary>
    /// Creates an entity from a prefab
    /// </summary>
    private void UpdateCreateEntity()
    {
        if (this._currentEntity != null || prefab == null) return;

        // Check, if clicking on UI or on the game world
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            // Get mouse position
            if (MouseUtil.TryRaycastAtMousePosition(out RaycastHit hit))
            {
                this._currentEntity = PrefabInstanceManager.Instance.Spawn(
                    this.prefab,
                    new Vector3(hit.point.x, 0, hit.point.z),
                    Quaternion.Euler(0, 90, 0)
                );
                this._outline = this._currentEntity.GetComponent<Outline>();
                this._outline.enabled = true;
            }
        }
        this._currentEntity.transform.parent = _walkable ? this.transform.GetChild(2) : this.transform;
    }

   
    /// <summary>
    /// Handle selected entity: cancel selection, rotate, set on place.
    /// </summary>
    private void UpdateCurrentEntity()
    {
        if (this._currentEntity == null) { return; }

        // Keep dock following the mouse.
        UpdateObjectPosition();

        // Pressing escape destroy dock not yet placed.
        if (Input.GetButtonDown("Cancel"))
        {
            CancelBuilding();

            return;
        }

        if (Input.GetButtonDown("KeyRotate")) { RotateBuilding(); }

        // After checking, if the position is available for building, build a dock pressing U.
        if (Input.GetButtonDown("RightMouseButton"))
        {
            BuildBuilding();
        }
    }
    
    private void CancelBuilding()
    {
        PrefabInstanceManager.Instance.DestroyEntity(this._currentEntity.GetInstanceID());
        this._currentEntity = null;
    }

    private void RotateBuilding()
    {
        // Rotate dock when Z or X are pressed.
        float rotation = Input.GetAxisRaw("KeyRotate") * 90;

        this._currentEntity.transform.Rotate(0, rotation, 0);
    }
    
    private void BuildBuilding()
    {
        if (!DoesEntityCollide())
        {
            this._outline.enabled = false;
            this._currentEntity   = null;
            if(_walkable)
                this._navMeshSurface.BuildNavMesh();
            UpdateCreateEntity();
        }
        else { Debug.Log("Can't build here!"); }
    }

    /// <summary>
    /// Keep created gameObject following the mouse position to select a position for it.
    /// </summary>
    private void UpdateObjectPosition()
    {
        // Use a raycast to register the position of the mouse
        if (MouseUtil.TryRaycastAtMousePosition(out RaycastHit hit))
        {
            Vector3 target = hit.point;

            IEnumerable<BoxCollider> snappables = FindObjectsOfType<BoxCollider>()
                                                           .Where(boxCollider => boxCollider != this._currentEntity.GetComponent<BoxCollider>());
            List<GameObject> objects = new List<GameObject>();
            foreach (var snappable in snappables)
            {
                objects.Add(snappable.gameObject);
            }

            // If left shift is not pressed, override the current raycasted target position with a possible snapping position.
            if (!Input.GetButton("IgnoreSnapping") && SnappingUtil.TryGetSnappingPoint(target, 3, .2f, this._currentEntity, objects, out Vector3 newTarget)) { target = newTarget; }

            this._currentEntity.transform.position = new Vector3(target.x, 0, target.z);
        }
    }

    /// <summary>
    /// Get all gameObjects colliding with the mouse position to check, if its possible to build here.
    /// </summary>
    /// <returns></returns>
    private bool DoesEntityCollide()
    {
        BoxCollider[] buildings = GameObject.FindObjectsOfType<BoxCollider>();
        
        Bounds bounds = this._currentEntity.GetComponent<BoxCollider>().bounds;
        
        return buildings.Where(building => building != this._currentEntity.GetComponent<BoxCollider>())
                       .Any(building => building.GetComponent<BoxCollider>().bounds.Intersects(bounds));
    }
}
