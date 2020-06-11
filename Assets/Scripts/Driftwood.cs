using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Util;

public class Driftwood : MonoBehaviour
{
    public GameObject driftwoodPrefab;
    public GameObject water;

    private readonly LinkedList<GameObject> _driftwood = new LinkedList<GameObject>();
    private int _gridSize;
    private float _timer;

    // Start is called before the first frame update
    void Start()
    {
        _gridSize = water.GetComponent<ProceduralGrid>().size;

        SpawnAlgorithm(30f, 80f);
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer <= 0)
            SpawnAlgorithm(100f, 300f);
        else
            _timer -= Time.deltaTime;

        foreach (GameObject driftwood in _driftwood.Where(driftwood => driftwood == null))
        {
            _driftwood.Remove(driftwood);
            break;
        }
    }

    /// <summary>
    /// Removes barrel from the list
    /// </summary>
    /// <param name="barrel">Barrel to be removed</param>
    public void RemoveBarrel(GameObject driftwood)
    {
        if (_driftwood.Contains(driftwood))
            _driftwood.Remove(driftwood);
    }

    /// <summary>
    /// Spawning algorithm that decides the new time and spawns the barrels at a random location.
    /// </summary>
    /// <param name="minTime">Minimum spawn time.</param>
    /// <param name="maxTime">Maximum spawn time.</param>
    private void SpawnAlgorithm(float minTime, float maxTime)
    {
        _timer = Random.Range(minTime, maxTime);

        int xPos = Random.Range(-_gridSize / 2, _gridSize / 2);
        int zPos = Random.Range(-_gridSize / 2, _gridSize / 2);

        SpawnDriftwood(5, new Vector3(xPos, 0, zPos));
    }

    /// <summary>
    /// Spawns the barrels at the centerpoint location.
    /// </summary>
    /// <param name="amount">Amount of barrels.</param>
    /// <param name="centrePoint">The position where the barrels spawn.</param>
    private void SpawnDriftwood(int amount, Vector3 centrePoint)
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
            Quaternion rotation = Quaternion.Euler(new Vector3(90, Random.Range(0, 90), 0));

            GameObject driftwood = PrefabInstanceManager.Instance.Spawn(driftwoodPrefab, randomPosition, rotation);
            BoxCollider barrelCollider = driftwood.GetComponent<BoxCollider>();

            if (!AnyCollision(barrelCollider, boxes))
            {
                _driftwood.AddLast(driftwood);
                driftwood.transform.parent = gameObject.transform;
            }
            else 
            { 
                Destroy(driftwood); 
            }
        }
    }
}