using System;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class Barrels : MonoBehaviour
{
    private readonly LinkedList<GameObject> barrels = new LinkedList<GameObject>();
    private NoiseGenerator waterLevels;
    private int gridSize;

    public GameObject barrelPrefab;
    public GameObject water;

    // Start is called before the first frame update
    void Start()
    {
        this.waterLevels = this.water.GetComponent<NoiseGenerator>();
        this.gridSize = this.water.GetComponent<ProceduralGrid>().size;

        SpawnBarrels(5);
    }

    // Update is called once per frame
    void Update()
    {
        Mesh mesh = this.waterLevels.MeshFilter.mesh;

        foreach (GameObject barrel in this.barrels)
        {
            Vector3 position = barrel.transform.position;

            barrel.transform.position = new Vector3(
                position.z,
                GetYForPosition(position, this.gridSize, mesh),
                position.z
            );
        }
    }

    private void SpawnBarrels(int amount)
    {
        Vector3 centrePoint = new Vector3(10, 0, 10);

        for (int i = 0; i < amount; i++)
        {
            Vector3 randomPosition = Vector3Util.RandomVector3InRange(centrePoint, 10, 0, 10);
            Quaternion rotation = Quaternion.Euler(Vector3Util.RandomVector3(90, 90, 0));


            GameObject barrel = PrefabInstanceManager.Instance.Spawn(this.barrelPrefab, randomPosition, rotation);
            this.barrels.AddLast(barrel);

            barrel.transform.parent = this.gameObject.transform;
        }
    }

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