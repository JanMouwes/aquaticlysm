using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Util;

public class Barrels : MonoBehaviour
{
    private readonly LinkedList<GameObject> _barrels = new LinkedList<GameObject>();
    private int _gridSize;

    public GameObject barrelPrefab;
    public GameObject water;

    private float _timer;

    // Start is called before the first frame update
    void Start()
    {
        this._gridSize = this.water.GetComponent<ProceduralGrid>().size;

        SpawnAlgorithm(30f, 80f);
    }

    // Update is called once per frame
    void Update()
    {
        if (this._timer <= 0)
            SpawnAlgorithm(100f, 500f);
        else
            this._timer -= Time.deltaTime;

        foreach (GameObject barrel in this._barrels.Where(barrel => barrel == null))
        {
            Debug.Log(barrel);
            this._barrels.Remove(barrel);

            break;
        }
    }

    /// <summary>
    /// Removes barrel from the list
    /// </summary>
    /// <param name="barrel">Barrel to be removed</param>
    public void RemoveBarrel(GameObject barrel)
    {
        if (this._barrels.Contains(barrel)) { this._barrels.Remove(barrel); }
    }

    /// <summary>
    /// Spawning algorithm that decides the new time and spawns the barrels at a random location.
    /// </summary>
    /// <param name="minTime">Minimum spawn time.</param>
    /// <param name="maxTime">Maximum spawn time.</param>
    private void SpawnAlgorithm(float minTime, float maxTime)
    {
        this._timer = Random.Range(minTime, maxTime);

        int xPos = Random.Range(-this._gridSize / 2, this._gridSize / 2);
        int zPos = Random.Range(-this._gridSize / 2, this._gridSize / 2);

        SpawnBarrels(5, new Vector3(xPos, 0, zPos));
    }

    /// <summary>
    /// Spawns the barrels at the centerpoint location.
    /// </summary>
    /// <param name="amount">Amount of barrels.</param>
    /// <param name="centrePoint">The position where the barrels spawn.</param>
    private void SpawnBarrels(int amount, Vector3 centrePoint)
    {
        bool DoesCollide(Collider colliderA, Collider colliderB) => colliderA.bounds.Intersects(colliderB.bounds);

        bool AnyCollision(Collider newCollider, IEnumerable<BoxCollider> possibleColliders)
        {
            return possibleColliders.Select(box => box.GetComponent<BoxCollider>())
                                    .Any(obj => DoesCollide(obj, newCollider));
        }

        BoxCollider[] boxes = FindObjectsOfType<BoxCollider>();

        for (int i = 0; i < amount; i++)
        {
            Vector3 randomPosition = Vector3Util.RandomVector3InRange(centrePoint, 10, 0, 10);
            Quaternion rotation = Quaternion.Euler(Vector3Util.RandomVector3(90, 90, 0));

            GameObject barrel = PrefabInstanceManager.Instance.Spawn(this.barrelPrefab, randomPosition, rotation);
            BoxCollider barrelCollider = barrel.GetComponent<BoxCollider>();

            if (!AnyCollision(barrelCollider, boxes))
            {
                this._barrels.AddLast(barrel);
                barrel.transform.parent = this.gameObject.transform;
            }
            else { Destroy(barrel); }
        }
    }
}