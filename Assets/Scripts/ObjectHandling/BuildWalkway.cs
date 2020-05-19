﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using Util;

/// <summary>
/// Class for building walkways
/// </summary>
public class BuildWalkway : Buildings
{
    private GameObject _currentEntity;
    private Outline _outline;
    private NavMeshSurface _navMeshSurface;

    public GameObject walkwayPrefab;

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
        if (!EventSystem.current.IsPointerOverGameObject() && MouseUtil.TryRaycastAtMousePosition(out RaycastHit hit))
        {
            this._currentEntity = PrefabInstanceManager.Instance.Spawn(
                this.walkwayPrefab,
                new Vector3(hit.point.x, 0, hit.point.z),
                Quaternion.Euler(0, 90, 0)
            );

            this._outline = this._currentEntity.GetComponent<Outline>();
            this._outline.enabled = true;
        }

        // Make sure, this walkway is assigned as a child of the Docks.
        this._currentEntity.transform.parent = this.transform;
    }

    /// <summary>
    /// Handle selected entity: cancel selection, rotate, set on place.
    /// </summary>
    private void UpdateCurrentEntity()
    {
        if (this._currentEntity == null) { return; }

        // Keep dock following the mouse.
        UpdateDockPosition();

        // Pressing escape destroys dock not yet placed.
        if (Input.GetButtonDown("Cancel"))
        {
            CancelBuilding();

            return;
        }

        // Pressing Z and X rotates the building 90 degrees on Y-axis.
        if (Input.GetButtonDown("KeyRotate")) { RotateBuilding(); }

        // After checking, if the position is available for building, build a dock pressing U.
        if (Input.GetButtonDown("KeyBuildHere")) { BuildBuilding(); }
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

    // After checking, if the position is available for building, build a dock pressing U.

    private void BuildBuilding()
    {
        if (!DoesEntityCollide())
        {
            this._outline.enabled = false;
            this._currentEntity = null;
            this._navMeshSurface.BuildNavMesh();
        }
        else { Debug.Log("Can't build here!"); }
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
            if (MouseUtil.TryRaycastAtMousePosition(out RaycastHit hit))
            {
                Vector3 target = hit.point;

                IEnumerable<GameObject> snappables = GameObject.FindGameObjectsWithTag("Walkway")
                                                               .Where(walkway => walkway != this._currentEntity);

                // If left shift is not pressed, override the current raycasted target position with a possible snapping position.
                if (!Input.GetButton("IgnoreSnapping") && SnappingUtil.TryGetSnappingPoint(target, 3, .2f, this._currentEntity, snappables, out Vector3 newTarget)) { target = newTarget; }

                this._currentEntity.transform.position = new Vector3(target.x, 0, target.z);
            }
        }
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
                          .Any(walkway => walkway.GetComponent<BoxCollider>().bounds.Intersects(bounds));
    }
}