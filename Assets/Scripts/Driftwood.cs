using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Util;

public class Driftwood : MonoBehaviour
{
    public GameObject driftwoodPrefab;
    public GameObject water;
    public float minTime;
    public float maxTime;

    private readonly LinkedList<GameObject> _driftwood = new LinkedList<GameObject>();
    private int _gridSize;
    private float _timer;

    // Start is called before the first frame update
    void Start()
    {
        _gridSize = water.GetComponent<ProceduralGrid>().size;

        SpawnRandomly(30f, 80f);
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer <= 0)
            SpawnRandomly(minTime, maxTime);
        else
            _timer -= Time.deltaTime;

        foreach (GameObject driftwood in _driftwood.Where(driftwood => driftwood == null))
        {
            _driftwood.Remove(driftwood);
            break;
        }
    }

    /// <summary>
    /// Spawning algorithm that decides the new time and spawns the driftwood at a random location.
    /// </summary>
    /// <param name="minDelay">Minimum spawn time.</param>
    /// <param name="maxDelay">Maximum spawn time.</param>
    private void SpawnRandomly(float minDelay, float maxDelay)
    {
        _timer = Random.Range(minDelay, maxDelay);

        float xPos = Random.Range(-_gridSize / 2.5f, _gridSize / 2.5f);
        float zPos = Random.Range(-_gridSize / 2.5f, _gridSize / 2.5f);

        SpawnDriftwood(5, new Vector3(xPos, 0, zPos));
    }

    /// <summary>
    /// Spawns the driftwood at the centerpoint location.
    /// </summary>
    /// <param name="amount">Amount of driftwood.</param>
    /// <param name="centrePoint">The position where the driftwood should spawn.</param>
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