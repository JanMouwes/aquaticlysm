using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralGrid : MonoBehaviour
{
    [Tooltip("Grid size (x and y).")] public int size;

    // Mesh filter
    private MeshFilter _filter;

    // Awake is called before Start
    void Awake()
    {
        if (size <= 0)
            size = 1;

        // Initialize filter
        _filter = GetComponent<MeshFilter>();
        _filter.mesh = GenerateMesh(this.size, -size * 0.5f);
    }

    /// <summary>
    /// Procedural plane generation
    /// </summary>
    private static Mesh GenerateMesh(int size, float offset)
    {
        // Create mesh
        Mesh mesh = new Mesh {name = "Procedural Grid"};

        // Create mesh data
        Vector3[] vertices = new Vector3[size * size];
        Vector3[] normals = new Vector3[size * size];
        Vector2[] uvs = new Vector2[size * size];

        // Generate vetices, normals and the uv
        for (
            int i = 0,
                x = 0;
            x < size;
            x++
        )
            for (int y = 0; y < size; y++, i++)
            {
                vertices[i] = GetVectorForPositionIndex(x, y, offset);
                normals[i] = Vector3.up;
                uvs[i] = new Vector2(x / (float) size, y / (float) size);
            }

        int[] triangles = GenerateTriangles(size);

        // Set and return the mesh
        mesh.SetVertices(vertices);
        mesh.SetNormals(normals);
        mesh.SetUVs(0, uvs);
        mesh.SetTriangles(triangles, 0);

        return mesh;
    }

    public static Vector3 GetVectorForPositionIndex(int xIndex, int yIndex, float offset)
    {
        return new Vector3(offset + xIndex, 0, offset + yIndex);
    }

    /// <summary>
    /// Generate the triangles used to display the contents on.
    /// </summary>
    /// <param name="size">Size of the grid.</param>
    /// <returns>Triangles positions.</returns>
    public static int[] GenerateTriangles(int size)
    {
        int[] triangles = new int[size * size * 6];
        int limit = size * size - size * 2;

        // Create two triangles for every square of vertices
        for (int i = 0,
                 vi = 0,
                 ti = 0;
            i <= limit;
            i++, vi++, ti += 6)
        {
            if ((vi + 1) % size == 0)
                vi++;

            triangles[ti] = vi;
            triangles[ti + 3] = triangles[ti + 2] = vi + size;
            triangles[ti + 4] = triangles[ti + 1] = vi + 1;
            triangles[ti + 5] = vi + size + 1;
        }

        return triangles;
    }
}