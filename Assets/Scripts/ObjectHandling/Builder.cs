using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using Util;

public class Builder : MonoBehaviour
{
    public GameObject[] prefabs;

    private NavMeshSurface _navMeshSurface;
    private IEnumerable<BoxCollider> _buildingBoxColliders;
    private GameObject _currentEntity;
    private GameObject _prefab;
    private Outline _outline;
    private bool _walkable;

    private void Awake()
    {
        GlobalStateMachine.instance.StateChanged += ToggleEnable;
        _navMeshSurface = transform.GetChild(2).GetComponent<NavMeshSurface>();
        _navMeshSurface.BuildNavMesh();
    }

    private void OnDisable()
    {
        CancelBuilding();
    }

    private void ToggleEnable(IState state) => enabled = state is Build;

    // Update is called once per frame
    private void Update()
    {

        // CURRENTLY TEST CODE, KEEP IN MIND.
        if (Input.GetKeyDown(KeyCode.L))
            ChangeBuildings(0, true);
        else if (Input.GetKeyDown(KeyCode.K))
            ChangeBuildings(1, false);

        if (Input.GetButtonDown("LeftMouseButton"))
            UpdateCreateEntity();

        UpdateCurrentEntity();
    }

    void ChangeBuildings(int index, bool isWalkable)
    {
        _prefab = prefabs[index];
        _walkable = isWalkable;
    }

    /// <summary>
    /// Creates an entity from a prefab.
    /// </summary>
    private void UpdateCreateEntity()
    {
        if (_currentEntity != null || _prefab == null) 
            return;

        _buildingBoxColliders = GetBoxColliders();

        // Check, if clicking on UI or on the game world
        if (!EventSystem.current.IsPointerOverGameObject() &&
            MouseUtil.TryRaycastAtMousePosition(out RaycastHit hit))
        {
            _currentEntity = PrefabInstanceManager.Instance.Spawn(
                _prefab,
                new Vector3(hit.point.x, 0, hit.point.z),
                Quaternion.Euler(0, 90, 0));

            _outline = _currentEntity.GetComponent<Outline>();
            _outline.enabled = true;
        }

        _currentEntity.transform.parent = _walkable ? transform.GetChild(2) : transform;
    }

    /// <summary>
    /// Handle selected entity: cancel selection, rotate, set on place.
    /// </summary>
    private void UpdateCurrentEntity()
    {
        if (_currentEntity == null)
            return;

        // Keep dock following the mouse.
        UpdateObjectPosition();

        // Pressing escape destroy dock not yet placed.
        if (Input.GetButtonDown("Cancel"))
        {
            CancelBuilding();
            return;
        }

        if (Input.GetButtonDown("KeyRotate"))
            RotateBuilding();

        // After checking, if the position is available for building, build a dock pressing U.
        if (Input.GetButtonDown("RightMouseButton"))
            BuildBuilding();
    }

    /// <summary>
    /// Cancels building a building and resets the builder.
    /// </summary>
    private void CancelBuilding()
    {
        if (_currentEntity != null)
        {
            PrefabInstanceManager.Instance.DestroyEntity(_currentEntity.GetInstanceID());
            _currentEntity = null;
        }

        _prefab = null;
    }

    /// <summary>
    /// Rotates the current building.
    /// </summary>
    private void RotateBuilding()
    {
        // Rotate dock when Z or X are pressed.
        float rotation = Input.GetAxisRaw("KeyRotate") * 90;
        _currentEntity.transform.Rotate(0, rotation, 0);
    }

    /// <summary>
    /// Build the building if it doesn't collide with any building.
    /// </summary>
    private void BuildBuilding()
    {
        if (!DoesEntityCollide())
        {
            _outline.enabled = false;
            _currentEntity = null;
            if (_walkable)
                _navMeshSurface.BuildNavMesh();
            UpdateCreateEntity();
        }
        else 
            Debug.Log("Can't build here!");
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

            // If left shift is not pressed, override the current raycasted target position with a possible snapping position.
            if (!Input.GetButton("IgnoreSnapping") && SnappingUtil.TryGetSnappingPoint(target, 3, .2f, _currentEntity, _buildingBoxColliders, out Vector3 newTarget))
                target = newTarget;

            _currentEntity.transform.position = new Vector3(target.x, 0, target.z);
        }
    }

    /// <summary>
    /// Get all gameObjects colliding with the mouse position to check, if its possible to build here.
    /// </summary>
    private bool DoesEntityCollide()
    {
        Bounds bounds = _currentEntity.GetComponent<BoxCollider>().bounds;
        return _buildingBoxColliders.Any(building => building.GetComponent<BoxCollider>().bounds.Intersects(bounds));
    }

    /// <summary>
    /// Gets the box colliders from all buildings.
    /// </summary>
    private IEnumerable<BoxCollider> GetBoxColliders() =>  FindObjectsOfType<BoxCollider>().Where(o => o != _currentEntity.GetComponent<BoxCollider>() && o.tag != "Character");

}
