using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class WaterPlane : MonoBehaviour
{
    // Plane size
    public int size;

    // Mesh filter
    private MeshFilter filter;

    // Awake is called before Start
    void Awake()
    {
        // Initialize filter
        filter = GetComponent<MeshFilter>();
        filter.mesh = GenerateMesh();
    }

    /// <summary>
    /// Procedural plane generation
    /// </summary>
    private Mesh GenerateMesh()
    {
        // Create mesh
        Mesh mesh = new Mesh();
        mesh.name = "Procedural Water Grid";

        // Create mesh data
        Vector3[] vertices = new Vector3[(size) * (size)];
        Vector3[] normals = new Vector3[(size) * (size)];
        Vector2[] uvs = new Vector2[(size) * (size )];

        // Generate vetices, normals and the uv
        for (int i = 0, x = 0; x < size; x++)
            for (int y = 0; y < size; y++, i++)
            {
                vertices[i] = new Vector3(-size * 0.5f + size * (x / (float)size), 0 , -size * 0.5f + size * (y / (float)size));
                normals[i] = (Vector3.up);
                uvs[i] = new Vector2(x / (float)size, y / (float)size);
            }

        int[] triangles = new int[size * size * 6];

        /*
        // Create two triangles for every square of vertices
        for (int i = 0, vi = 0, x = 0; x < size; x++, vi++)
            for (int y = 0; y < size; y++, i += 6, vi++)
            {
                triangles[i] = vi;
                triangles[i + 3] = triangles[i + 2] = vi + 1;
                triangles[i + 4] = triangles[i + 1] = vi + size;
                triangles[i + 5] = vi + size + 1;
            }
        */

        // Create two triangles for every square of vertices
        for (int i = 0, vi = 0, ti = 0; i <= size * size - size * 2; i++, vi++, ti+=6)
        {
            Debug.Log("i: " +  i);

            //Debug.Log(size * size + size);
            if ((vi + 1) % size == 0)
                vi++;

            triangles[ti] = vi;
            triangles[ti + 3] = triangles[ti + 2] = vi + size;
            triangles[ti + 4] = triangles[ti + 1] = vi + 1;
            triangles[ti + 5] = vi + size + 1;
        }

        // Set and return the mesh
        mesh.SetVertices(vertices);
        mesh.SetNormals(normals);
        mesh.SetUVs(0, uvs);
        mesh.SetTriangles(triangles, 0);
        return mesh;
    }
}
