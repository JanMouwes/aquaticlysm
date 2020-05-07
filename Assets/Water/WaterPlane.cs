﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class WaterPlane : MonoBehaviour
{
    public int xSize, ySize;

    private MeshFilter filter;

    void Awake()
    {
        filter = GetComponent<MeshFilter>();
        filter.mesh = GenerateMesh();
        
    }

    private Mesh GenerateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.name = "Procedural Water Grid";

        Vector3[] vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        Vector3[] normals = new Vector3[(xSize + 1) * (ySize + 1)];
        Vector2[] uvs = new Vector2[(xSize + 1) * (ySize + 1)];

        for (int i = 0, x = 0; x < xSize; x++)
            for (int y = 0; y < ySize; y++, i++)
            {
                vertices[i] = new Vector3(-xSize * 0.5f + xSize * (x / (float)xSize), 0 , -ySize * 0.5f + ySize * (y / (float)ySize));
                normals[i] = (Vector3.up);
                uvs[i] = new Vector2(x / (float)xSize, y / (float)ySize);
            }

        int[] triangles = new int[xSize * ySize * 6];

        for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++)
            for (int x = 0; x < xSize; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                triangles[ti + 5] = vi + xSize + 2;
            }

        mesh.SetVertices(vertices);
        mesh.SetNormals(normals);
        mesh.SetUVs(0, uvs);
        mesh.SetTriangles(triangles, 0);
        return mesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
