using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

/// <summary>
/// Class for building walkways
/// </summary>
public class BuildWalkway : MonoBehaviour
{
    private GameObject _currentEntity;
    private NavMeshSurface _navMeshSurface;

    public GameObject walkwayPrefab;

    /// <summary>
    /// Highlights when hovering the entity
    /// </summary>
    public Material selectedMaterial;

    /// <summary>
    /// Actual material when entity is set down
    /// </summary>
    public Material normalMaterial;

    // Start is called before the first frame update
    private void Awake()
    {
        this._navMeshSurface = GetComponent<NavMeshSurface>();
        this._navMeshSurface.BuildNavMesh();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateCreateEntity();

        UpdateCurrentEntity();
    }

    /// <summary>
    /// Creates an entity from a prefab
    /// </summary>
    private void UpdateCreateEntity()
    {
        if (!Input.GetButtonDown("CreateNewObject") || this._currentEntity != null) return;

        // Check, if clicking on UI or on the game world
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            // Get mouse position
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                GameObject entity = PrefabInstanceManager.Instance.Spawn(
                    this.walkwayPrefab,
                    new Vector3(hit.point.x, 0, hit.point.z),
                    Quaternion.Euler(0, 90, 0)
                );

                this._currentEntity = entity;
                SwitchMaterial(true);
            }
        }

        this._currentEntity.transform.parent = this.transform;
    }

    /// <summary>
    /// 
    /// </summary>
    private void UpdateCurrentEntity()
    {
        if (this._currentEntity == null) { return; }

        // Keep dock following the mouse.
        UpdateDockPosition();

        // Pressing escape destroy dock not yet placed.
        if (Input.GetButtonDown("Cancel"))
        {
            PrefabInstanceManager.Instance.DestroyEntity(this._currentEntity.GetInstanceID());
            this._currentEntity = null;

            return;
        }


        if (Input.GetButtonDown("KeyRotate"))
        {
            // Rotate dock when Z or X are pressed.
            float rotation = Input.GetAxisRaw("KeyRotate") * 90;

            this._currentEntity.transform.Rotate(0, rotation, 0);
        }

        // After checking, if the position is available for building, build a dock pressing U.
        if (Input.GetButtonDown("KeyBuildHere"))
        {
            if (DoesEntityCollide())
            {
                SwitchMaterial(false);
                this._currentEntity = null;
                this._navMeshSurface.BuildNavMesh();
            }
            else { Debug.Log("Can't build here!"); }
        }
    }

    /// <summary>
    /// Keep created gameObject following the mouse position to select a position for it.
    /// </summary>
    private void UpdateDockPosition()
    {
        // Check, if clicking on the UI or the game world.
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            // Use a raycast to register the position of the mouse
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit)) { this._currentEntity.transform.position = new Vector3(hit.point.x, 0, hit.point.z); }
        }
    }

    /// <summary>
    /// Emphasize with an orange highlighter, if currently raycasted gameObject is selected or not.
    /// </summary>
    /// <param name="selected"></param>
    private void SwitchMaterial(bool selected)
    {
        MeshRenderer gameObjectRenderer = this._currentEntity.GetComponent<MeshRenderer>();

        // If gameObject is created just now, use highlighted material.
        gameObjectRenderer.material = selected ? this.selectedMaterial : this.normalMaterial;
    }

    /// <summary>
    /// Get all gameObjects colliding with the mouse position to check, if its possible to build here.
    /// </summary>
    /// <returns></returns>
    private bool DoesEntityCollide()
    {
        GameObject[] allWalkways = GameObject.FindGameObjectsWithTag("Walkway");

        Bounds bounds = this._currentEntity.GetComponent<BoxCollider>().bounds;

        return allWalkways.Where(walkway => walkway != this._currentEntity)
                          .All(walkway => !walkway.GetComponent<BoxCollider>().bounds.Intersects(bounds));
    }
}