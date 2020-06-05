﻿using Actions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using Util;

public class Builder : MonoBehaviour, IActionComponent
{
    public GameObject[] prefabs;

    private GameActionButtonModel[] _buttonModels;
    private NavMeshSurface[] _navMeshSurfaces;
    private IEnumerable<BoxCollider> _buildingBoxColliders;
    private GameObject _currentEntity;
    private GameObject _prefab;
    private Outline _outline;
    private Color _originalOutlineColour;
    private bool _walkable;

    public IEnumerable<GameActionButtonModel> ButtonModels => _buttonModels;

    private void Awake()
    {
        _navMeshSurfaces = FindObjectsOfType<NavMeshSurface>();
    }

    private void Start()
    {
        BuildNavMeshes();
        _buttonModels = GetGameActionButtonModels(this).ToArray();
    }

    private void Update()
    {
        if (_currentEntity == null)
            return;

        UpdatePosition();

        if (Input.GetButtonDown("KeyRotate"))
            Rotate();

        // After checking, if the position is available for building, build a dock pressing U.
        if (Input.GetButtonDown("RightMouseButton"))
            BuildEntity();

        // Pressing escape destroy dock not yet placed.
        if (Input.GetButtonDown("Cancel"))
            CancelBuilding();
    }

    void ChangePrefab(int index, bool isWalkable)
    {
        _prefab = prefabs[index];
        _walkable = isWalkable;
    }

    /// <summary>
    /// Creates an entity from a prefab.
    /// </summary>
    private void CreateEntity()
    {
        if (_currentEntity != null || _prefab == null)
            return;

        _buildingBoxColliders = GetBoxColliders();

        // Check, if clicking on UI or on the game world
        if (MouseUtil.TryRaycastAtMousePosition(out RaycastHit hit))
        {
            _currentEntity = PrefabInstanceManager.Instance.Spawn(
                _prefab,
                new Vector3(hit.point.x, 0, hit.point.z),
                Quaternion.Euler(0, 90, 0));

            _outline = _currentEntity.GetComponent<Outline>();
            _outline.enabled = true;
            this._originalOutlineColour = this._outline.OutlineColor;
        }

        _currentEntity.transform.parent = _walkable ? transform.GetChild(0) : transform.GetChild(1);
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

        GlobalStateMachine.instance.ChangeState(new PlayState());
    }

    /// <summary>
    /// Rotates the current building.
    /// </summary>
    private void Rotate()
    {
        // Rotate dock when Z or X are pressed.
        float rotation = Input.GetAxisRaw("KeyRotate") * 90;
        _currentEntity.transform.Rotate(0, rotation, 0);
    }

    /// <summary>
    /// Build the building if it doesn't collide with any building.
    /// </summary>
    private void BuildEntity()
    {
        if (!DoesEntityCollide())
        {
            this._outline.OutlineColor = this._originalOutlineColour;
            _outline.enabled = false;
            _currentEntity = null;
            
            BuildNavMeshes();

            // Create next entity
            CreateEntity();
        }
        else
            Debug.Log("Can't build here!");
    }

    /// <summary>
    /// Keep created gameObject following the mouse position to select a position for it.
    /// </summary>
    private void UpdatePosition()
    {
        // Use a raycast to register the position of the mouse
        if (MouseUtil.TryRaycastAtMousePosition(out RaycastHit hit))
        {
            Vector3 target = hit.point;

            // If ignore button is not pressed, override the current ray casted target position with a possible snapping position.
            if (!Input.GetButton("IgnoreSnapping") && SnappingUtil.TryGetSnappingPoint(target, 3, .1f, _currentEntity, _buildingBoxColliders, out Vector3 newTarget))
                target = newTarget;
        
            _currentEntity.transform.position = new Vector3(target.x, 0, target.z);

            // Building legality feedback for player
            this._outline.OutlineColor = DoesEntityCollide() ? Color.red : Color.green;
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
    /// rebuild all navmeshes in the scene
    /// </summary>
    private void BuildNavMeshes()
    {
        foreach (NavMeshSurface navMeshSurface in _navMeshSurfaces)
        {
            navMeshSurface.BuildNavMesh();
        }
    }

    /// <summary>
    /// Gets the box colliders from all buildings.
    /// </summary>
    private IEnumerable<BoxCollider> GetBoxColliders() => FindObjectsOfType<BoxCollider>().Where(o => o != _currentEntity.GetComponent<BoxCollider>() && o.tag != "Character");

    public bool HandleAction(RaycastHit hit, bool priority)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// All button actions.
    /// </summary>
    /// <param name="owner">The boat.</param>
    private static IEnumerable<GameActionButtonModel> GetGameActionButtonModels(Builder builder)
    {
        yield return new GameActionButtonModel()
        {
            Name = "BuildWalkway",
            Icon = UnityEngine.Resources.Load<Sprite>("Icons/Road"),
            OnClick = () => builder.Build(0, true)
        };        
        yield return new GameActionButtonModel()
        {
            Name = "BuildBarge",
            Icon = UnityEngine.Resources.Load<Sprite>("Icons/Barge"),
            OnClick = () => builder.Build(1, false)
        };      
        yield return new GameActionButtonModel()
        {
            Name = "BuildSolarPanel",
            Icon = UnityEngine.Resources.Load<Sprite>("Icons/SolarPanel"),
            //OnClick = () => builder.Build(0, false)
        };      
        yield return new GameActionButtonModel()
        {
            Name = "BuildBattery",
            Icon = UnityEngine.Resources.Load<Sprite>("Icons/Battery"),
            //OnClick = () => builder.Build(0, false)
        };      
        yield return new GameActionButtonModel()
        {
            Name = "BuildHouse",
            Icon = UnityEngine.Resources.Load<Sprite>("Icons/House"),
            //OnClick = () => builder.Build(0, false)
        };
    }

    public void Build(int index, bool walkable) 
    {
        GlobalStateMachine.instance.ChangeState(new BuildState());
        ChangePrefab(index, walkable);
        CreateEntity();
    }
}