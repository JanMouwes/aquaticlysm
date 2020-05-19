﻿using System;
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

    private void Start()
    {
        enabled = true;
        GlobalStateMachine.instance.StateChanged += Changestate;
    }

    // Start is called before the first frame update
    private void Awake()
    {
        this._navMeshSurface = transform.GetChild(2).GetComponent<NavMeshSurface>();
        this._navMeshSurface.BuildNavMesh();
    }

    private void Changestate(IState state) =>this.enabled = (state is Build);
    
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
        if (Input.GetKeyDown(KeyCode.L))
        {
            ChangeBuildings(0);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            ChangeBuildings(1);
        }
        
        if (!Input.GetButtonDown("CreateNewObject") || this._currentEntity != null) return;

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
        
        this._currentEntity.transform.parent = this.transform.GetChild(2);
    }

    void ChangeBuildings(int index)
    {
        prefab = GameObjects[index];
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
            if (!DoesEntityCollide())
            {
                this._outline.enabled = false;
                this._currentEntity = null;
                this._navMeshSurface.BuildNavMesh();
            }
            else { Debug.Log("Can't build here!"); }
        }
    }

    /// <summary>
    /// Keep created gameObject following the mouse position to select a position for it.
    /// </summary>
    private void UpdateObjectPosition()
    {
        // Check, if clicking on the UI or the game world.
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            // Use a raycast to register the position of the mouse
            if (MouseUtil.TryRaycastAtMousePosition(out RaycastHit hit))
            {
                this._currentEntity.transform.position = new Vector3(hit.point.x, 0, hit.point.z);
            }
        }
    }

    /// <summary>
    /// Get all gameObjects colliding with the mouse position to check, if its possible to build here.
    /// </summary>
    /// <returns></returns>
    private bool DoesEntityCollide()
    {
        // Buildings[] buildings = GameObject.FindObjectsOfType<Buildings>();
        //
        // Bounds bounds = this._currentEntity.GetComponent<BoxCollider>().bounds;
        //
        // return buildings.Where(building => building != this._currentEntity)
        //                .Any(building => building.GetComponent<BoxCollider>().bounds.Intersects(bounds));
        return false;
    }
}
