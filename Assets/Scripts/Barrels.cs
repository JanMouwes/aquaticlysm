using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Util;

public class Barrels : MonoBehaviour
{
    private readonly LinkedList<GameObject> _barrels = new LinkedList<GameObject>();
    private NoiseGenerator _waterLevels;
    private int _gridSize;

    public GameObject barrelPrefab;
    public GameObject water;

    // Start is called before the first frame update
    void Start()
    {
        this._waterLevels = this.water.GetComponent<NoiseGenerator>();
        this._gridSize = this.water.GetComponent<ProceduralGrid>().size;

        SpawnBarrels(5, new Vector3(10, 0, 10));
    }

    // Update is called once per frame
    void Update()
    {
        Mesh mesh = this._waterLevels.MeshFilter.mesh;

        foreach (GameObject barrel in this._barrels)
        {
            Vector3 position = barrel.transform.position;

            barrel.transform.position = new Vector3(
                position.x,
                GetYForPosition(position, this._gridSize, mesh),
                position.z
            );
        }
    }

    /// <summary>
    /// Spawns the barrels at the centerpoint location.
    /// </summary>
    /// <param name="amount">Amount of barrels.</param>
    /// <param name="centrePoint">The position where the barrels spawn.</param>
    private void SpawnBarrels(int amount, Vector3 centrePoint)
    {
        BoxCollider[] boxes = FindObjectsOfType<BoxCollider>();

        for (int i = 0; i < amount; i++)
        {
            Vector3 randomPosition = Vector3Util.RandomVector3InRange(centrePoint, 10, 0, 10);
            Quaternion rotation = Quaternion.Euler(Vector3Util.RandomVector3(90, 90, 0));

            GameObject barrel = PrefabInstanceManager.Instance.Spawn(this.barrelPrefab, randomPosition, rotation);

            if(!boxes.Any(obj => obj.GetComponent<BoxCollider>().bounds
                .Intersects(barrel.GetComponent<BoxCollider>().bounds)))
            {
                this._barrels.AddLast(barrel);
                barrel.transform.parent = this.gameObject.transform;
            }
            else
            {
                Destroy(barrel);
            }
        }
    }

    /// <summary>
    /// Get the Y position for the barrel.
    /// </summary>
    /// <param name="position">Barrel position.</param>
    /// <param name="gridSize">Size of the grid.</param>
    /// <param name="mesh">The watermesh of the grid.</param>
    private static float GetYForPosition(Vector3 position, int gridSize, Mesh mesh)
    {
        Vector3[] newNeighbours = GetNearestFourNeighbours(position, gridSize, mesh);

        float distToX = position.x - newNeighbours[0].x;
        float distToZ = position.z - newNeighbours[0].z;

        float topSlope = newNeighbours[1].y - newNeighbours[0].y;
        float bottomSlope = newNeighbours[3].y - newNeighbours[2].y;

        float topY = topSlope * distToX;
        float bottomY = bottomSlope * distToX;

        float zSlope = bottomY - topY;
        float actualY = zSlope * distToZ;

        return actualY;
    }

    /// <summary>
    /// Get the nearest four neighbours nodes for the barrel.
    /// </summary>
    /// <param name="position">Barrel position.</param>
    /// <param name="gridSize">Size of the grid.</param>
    /// <param name="mesh">The watermesh of the grid.</param>
    private static Vector3[] GetNearestFourNeighbours(Vector3 position, int gridSize, Mesh mesh)
    {
        int indexOffset = (gridSize * (gridSize + 1)) / 2;
        int index1 = indexOffset + (int) (Math.Floor(position.z) + gridSize * Math.Floor(position.x));

        int[] neighbourIndices =
        {
            index1,
            index1 + 1,
            index1 + gridSize,
            index1 + gridSize + 1
        };

        Vector3[] newNeighbours =
        {
            mesh.vertices[neighbourIndices[0]],
            mesh.vertices[neighbourIndices[1]],
            mesh.vertices[neighbourIndices[2]],
            mesh.vertices[neighbourIndices[3]],
        };

        return newNeighbours;
    }
}