using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

/// <summary>
/// Class for building walkways
/// </summary>
public class BuildWalkway : MonoBehaviour
{
    private GameObject _entity;
    private NavMeshSurface _navMeshSurface;
    private int _layer = 8;

    public GameObject walkwayGameObject;
    public Material selectedMaterial;
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

        if (Input.GetButtonDown("CreateNewObject") && this._entity == null)
        {
            // Check, if clicking on UI or on the game world
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                RaycastHit hit;

                // Get mouse position
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {

                    GameObject entity = PrefabInstanceManager.Instance.Spawn(
                        this.walkwayGameObject,
                        new Vector3(hit.point.x, 0, hit.point.z),
                        Quaternion.Euler(0, 90, 0)
                        );

                    this._entity = entity;
                    SwitchMaterial(true);

                }
            }
            this._entity.transform.parent = this.transform;
        }

        if (this._entity != null)
        {
            // Keep dock following the mouse.
            UpdateDockPosition();

            // Pressing escape destroy dock not yet placed.
            if (Input.GetButtonDown("Cancel"))
            {
             
                PrefabInstanceManager.Instance.DestroyEntity(this._entity.GetInstanceID());
                this._entity = null;

            }
            
            // Rotate dock when Z or X are pressed.
            float rotation = Input.GetAxisRaw("KeyRotate") * 90;
            if (Input.GetButtonDown("KeyRotate"))
                _entity.transform.Rotate(0, rotation, 0);

            // After checking, if the position is available for building, build a dock pressing U.
            if (Input.GetButtonDown("KeyBuildHere"))
            {

                if (CheckForCollision())
                {
                    SwitchMaterial(false);
                    this._entity.layer = _layer;
                    this._entity = null;
                    this._navMeshSurface.BuildNavMesh();
                }
                else
                {
                    Debug.Log("Cant build here!");
                }

            }
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
            RaycastHit hit;

            // Use a raycast to register the position of the mouse
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                _entity.transform.position = new Vector3(hit.point.x, 0, hit.point.z);
            }
        }
    }

    /// <summary>
    /// Emphasize with an orange highlighter, if currently raycasted gameObject is selected or not.
    /// </summary>
    /// <param name="selected"></param>
    private void SwitchMaterial(bool selected)
    {
        MeshRenderer gameObjectRenderer = _entity.GetComponent<MeshRenderer>();

        // If gameObject is created just now, use highlighted material.
        if (selected)
            gameObjectRenderer.material = selectedMaterial;
        else
            gameObjectRenderer.material = normalMaterial;

    }

    /// <summary>
    /// Get all gameObjects colliding with the mouse position to check, if its possible to build here.
    /// </summary>
    /// <returns></returns>
    private bool CheckForCollision()
    {
        // Collect all hits from the raycast
        RaycastHit[] hits;
        hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];

            // If gameObject is in layer "Building", cannot build here.
            if (hits[i].collider.gameObject.layer == _layer)
            {
                return false;
            }
        }
        return true;
    }
}